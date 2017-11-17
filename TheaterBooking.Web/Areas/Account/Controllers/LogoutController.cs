using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System.Web.Mvc;

namespace TheaterBooking.Web.Areas.Account.Controllers
{
    /// <summary>
    ///     Controller for the logout process
    /// </summary>
    public class LogoutController : Controller
    {
        private readonly IAuthenticationManager _authenticationManager;

        /// <summary>
        ///     Constructs an instance of the logout controller using the specified authentication manager.
        /// </summary>
        /// <param name="authenticationManager">The manager used to control authentication</param>
        public LogoutController(IAuthenticationManager authenticationManager)
        {
            _authenticationManager = authenticationManager;
        }

        /// <summary>
        ///     Logs out the current user
        /// </summary>
        /// <returns>A view of the home page</returns>
        [HttpPost]
        [Authorize]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Index()
        {
            _authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie, "Google");
            return RedirectToRoute("Default");
        }
    }
}
