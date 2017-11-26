using System.Linq;
using System.Web.Mvc;
using TheaterBooking.Web.Database;

namespace TheaterBooking.Web.Areas.Movies.Controllers
{
    /// <summary>
    ///     Controller for the movie listing page
    /// </summary>
    public class ListingController : Controller
    {
        private readonly TheaterDbEntities _db;

        /// <summary>
        ///     Instantiates a new listing controller with the specified database model
        /// </summary>
        public ListingController(TheaterDbEntities db)
        {
            _db = db;
        }

        /// <summary>
        ///     Gets the listing index page
        /// </summary>
        /// <returns>A view of the index page</returns>
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View(_db.Movies
                .Include(nameof(Movie.Rating))
                .Include(nameof(Movie.Genres))
                .Include(nameof(Movie.Showtimes))
                .ToList());
        }
    }
}
