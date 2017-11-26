using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TheaterBooking.Web.Database;

namespace TheaterBooking.Web.Areas.Movies.Controllers
{
    /// <summary>
    ///     Controller for the movie preview page
    /// </summary>
    public class PreviewController : Controller
    {
        private readonly TheaterDbEntities _db;

        /// <summary>
        ///     Instantiates a new preview controller with the specified database model
        /// </summary>
        public PreviewController(TheaterDbEntities db)
        {
            _db = db;
        }

        /// <summary>
        ///     Gets the home index page
        /// </summary>
        /// <returns>A view of the index page</returns>
        [AllowAnonymous]
        public ActionResult Index(int movieId)
        {
            var movie = _db.Movies.Where(m => m.Movie_ID == movieId)
                .Include(m => m.Showtimes)
                .SingleOrDefault();
            if (movie == null)
            {
                throw new HttpException((int)HttpStatusCode.NotFound, "Movie not found");
            }

            return View(movie);
        }
    }
}
