using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using TheaterBooking.Web.Areas.Account.Models;
using TheaterBooking.Web.Users;
using TheaterBooking.Web.Utilities;

namespace TheaterBooking.Web.Areas.Account.Controllers
{
    /// <summary>
    ///     Controller for the registration page
    /// </summary>
    [AllowAnonymous]
    public class RegistrationController : Controller
    {
        private readonly SignInManager<User, string> _signinManager;
        private readonly UserManager<User, string> _userManager;

        /// <summary>
        ///     Constructs an instance of the registration controller using the specified user manager.
        /// </summary>
        /// <param name="signinManager">The manager used to control application sign ins</param>
        /// <param name="userManager">The manager used to control user registrations</param>
        public RegistrationController(SignInManager<User, string> signinManager, UserManager<User, string> userManager)
        {
            _signinManager = signinManager;
            _userManager = userManager;
        }

        /// <summary>
        ///     Gets the account registration page
        /// </summary>
        /// <returns>A view of the account registration page</returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        ///     Attempts to register the user specified in the model
        /// </summary>
        /// <param name="model">The registration view model containing the username and password</param>
        /// <returns>A view of the resulting confirmation page</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(RegistrationViewModel model)
        {
            using (var hash = SHA256.Create())
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                var user = new User
                {
                    UserName = Base62.Encode(hash.ComputeHash(Encoding.UTF8.GetBytes(model.Email))),
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName
                };

                if (!(await _userManager.CreateAsync(user, model.Password)).IsValid(ModelState))
                {
                    return View(model);
                }

                await _signinManager.SignInAsync(user, false, false);
                
                if (!(await _userManager.UpdateAsync(user)).IsValid(ModelState))
                {
                    return View(model);
                }

                return RedirectToRoute("Default");
            }
        }
    }
}
