using System.Web.Mvc;
using TheaterBooking.Web.Utilities;

namespace TheaterBooking.Web.Areas.Account.Controllers
{
    /// <summary>
    ///     Controller for the home page
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        ///     Gets the home index page
        /// </summary>
        /// <returns>A view of the index page</returns>
        [AllowAnonymous]
        public ActionResult Index()
        {
            return new TransferResult("Index", "Listing", new { area = "Movies" });
        }
    }
}
