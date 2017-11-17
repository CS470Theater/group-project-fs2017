using System.Web.Mvc;

namespace TheaterBooking.Web.Areas.Movies.Controllers
{
    /// <summary>
    ///     Controller for the movie preview page
    /// </summary>
    public class PreviewController : Controller
    {
        /// <summary>
        ///     Gets the home index page
        /// </summary>
        /// <returns>A view of the index page</returns>
        [Authorize(Roles = "web.home.view")]
        public ActionResult Index()
        {
            return View();
        }
    }
}
