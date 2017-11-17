using TheaterBooking.Core.Users;
using TheaterBooking.Web.Areas.Account.Models;
using Castle.Core.Logging;
using JetBrains.Annotations;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Newtonsoft.Json;
using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.EntityFramework;
using TheaterBooking.Web.Utilities;

namespace TheaterBooking.Web.Areas.Account.Controllers
{
    /// <summary>
    ///     Controller for the login page
    /// </summary>
    [AllowAnonymous]
    public class LoginController : Controller
    {
        private readonly Lazy<IAuthenticationManager> _authenticationManager;
        private readonly Lazy<SignInManager<User, string>> _signinManager;
        private readonly Lazy<UserManager<User, string>> _userManager;

        /// <summary>
        ///     Constructs an instance of the login controller using the specified managers.
        /// </summary>
        /// <param name="authenticationManager">The manager used to control authentication</param>
        /// <param name="signinManager">The manager used to control application sign ins</param>
        /// <param name="userManager">The manager used to control user registrations</param>
        public LoginController(Lazy<IAuthenticationManager> authenticationManager,
            Lazy<SignInManager<User, string>> signinManager, Lazy<UserManager<User, string>> userManager)
        {
            _signinManager = signinManager;
            _authenticationManager = authenticationManager;
            _userManager = userManager;
        }

        /// <summary>
        ///     Gets or sets the logging implementation
        /// </summary>
        [UsedImplicitly(ImplicitUseKindFlags.Assign)]
        public ILogger Logger { private get; set; } = NullLogger.Instance;

        /// <summary>
        ///     Gets the account login page
        /// </summary>
        /// <returns>A view of the account login page</returns>
        [AnonymousOnly]
        public ActionResult Index([CanBeNull] string salt, [CanBeNull] string redirectUri, [CanBeNull] string openIdErrorMessage)
        {
            if (openIdErrorMessage != null)
            {
                ModelState.AddModelError("", openIdErrorMessage);
            }

            return View(new LoginViewModel {Salt = salt, RedirectUri = redirectUri});
        }
        
        /// <summary>
        ///     Begins an external login using an OAuth2 provider
        /// </summary>
        /// <param name="model">The login view model containing the salt and redirect Uri</param>
        /// <param name="provider">The external OAuth2 provider to authenticate with</param>
        /// <returns>A challenge response to send to the external provider</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AnonymousOnly]
        public HttpUnauthorizedResult ExternalLogin([NotNull] LoginViewModel model, string provider)
        {
            return new ExternalLoginChallenge(_authenticationManager.Value,
                Url.Action("ExternalLoginCallback", new {salt = model.Salt, redirectUri = model.RedirectUri}), provider);
        }

        /// <summary>
        ///     Completes an external login using an OAuth2 provider
        /// </summary>
        /// <returns>A view of the login confirmation page</returns>
        [AnonymousOnly]
        [ActionName("ExternalLoginCallback")]
        public async Task<ActionResult> ExternalLoginCallbackAsync([CanBeNull] string salt, [CanBeNull] string redirectUri)
        {
            try
            {
                var info = await _authenticationManager.Value.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    Logger.Error("An unsuccessful login occurred with an external login service.");
                    ModelState.AddModelError("", "Unable to login with that external login service.");
                    return View("Index", new LoginViewModel {Salt = salt, RedirectUri = redirectUri});
                }

                var result = await _signinManager.Value.ExternalSignInAsync(info, false);
                switch (result)
                {
                    case SignInStatus.LockedOut:
                    case SignInStatus.RequiresVerification:
                        throw new NotImplementedException($"{result} not implemented");
                    case SignInStatus.Success:
                        return Success(salt, redirectUri);
                    case SignInStatus.Failure:
                        info.Email = info.Email ?? info.ExternalIdentity.FindFirst("preferred_username")?.Value;

                        var user = new User
                        {
                            UserName = Base62.Encode(Base64.UrlSafeDecode(new RandomStringFactory().Generate())),
                            Email = info.Email
                        };
                        
                        if (!(await _userManager.Value.CreateAsync(user)).IsValid(ModelState))
                        {
                            return View("Index", new LoginViewModel { Salt = salt, RedirectUri = redirectUri });
                        }

                        if (!(await _userManager.Value.AddLoginAsync(user.Id, info.Login)).IsValid(ModelState))
                        {
                            return View("Index", new LoginViewModel { Salt = salt, RedirectUri = redirectUri });
                        }
                        
                        if (!(await _userManager.Value.UpdateAsync(user)).IsValid(ModelState))
                        {
                            return View("Index", new LoginViewModel { Salt = salt, RedirectUri = redirectUri });
                        }

                        await _signinManager.Value.SignInAsync(user, false, false);

                        return Success(salt, redirectUri);
                    default:
                        throw new NotImplementedException($"{result} not implemented");
                }
            }
            catch (DbEntityValidationException e)
            {
                Logger.Error(JsonConvert.SerializeObject(e.EntityValidationErrors.Select(error => new {error.Entry.Entity, error.ValidationErrors})));
                throw;
            }
        }

        private ActionResult Success([CanBeNull] string salt, [CanBeNull] string redirectUri)
        {
            if (salt == null || redirectUri == null)
            {
                return RedirectToRoute("Default");
            }

            var crypto = new ExternalCrypto(salt);
            return Redirect(crypto.Decrypt(redirectUri));
        }

        /// <summary>
        ///     An OAuth2 external login challenge
        /// </summary>
        private class ExternalLoginChallenge : HttpUnauthorizedResult
        {
            private readonly IAuthenticationManager _authenticationManager;
            private readonly string _loginProvider;
            private readonly string _redirectUri;

            /// <summary>
            ///     Constructs a new instance of the OAuth2 external login challenge result
            /// </summary>
            /// <param name="authenticationManager">The authentication manager used for authentication</param>
            /// <param name="redirectUri">The callback Uri</param>
            /// <param name="loginProvider">The name of the external login provider being used</param>
            internal ExternalLoginChallenge(IAuthenticationManager authenticationManager, string redirectUri,
                string loginProvider)
            {
                _authenticationManager = authenticationManager;
                _redirectUri = redirectUri;
                _loginProvider = loginProvider;
            }

            /// <summary>
            ///     Enables processing of the result of an action method by a custom type that inherits from the
            ///     <see cref="T:System.Web.Mvc.ActionResult" /> class.
            /// </summary>
            /// <param name="context">
            ///     The context in which the result is executed. The context information includes the controller, HTTP content, request
            ///     context, and route data.
            /// </param>
            public override void ExecuteResult(ControllerContext context)
            {
                _authenticationManager.Challenge(new AuthenticationProperties {RedirectUri = _redirectUri},
                    _loginProvider);
            }
        }
    }
}
