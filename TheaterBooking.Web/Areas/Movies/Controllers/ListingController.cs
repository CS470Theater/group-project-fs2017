using System.Web.Mvc;

namespace TheaterBooking.Web.Areas.Movies.Controllers
{
    /// <summary>
    ///     Controller for the movie listing page
    /// </summary>
    public class ListingController : Controller
    {
        /// <summary>
        ///     Gets the listing index page
        /// </summary>
        /// <returns>A view of the index page</returns>
        [Authorize(Roles="web.home.view")]
        public ActionResult Index()
        {
            return View();
        }
    }
}
