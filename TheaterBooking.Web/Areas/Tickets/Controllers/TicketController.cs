using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TheaterBooking.Web.Areas.Tickets.Models;
using TheaterBooking.Web.Database;

namespace TheaterBooking.Web.Areas.Tickets.Controllers
{
    public class TicketController : Controller
    {
        private readonly TheaterDbEntities _db;

        /// <summary>
        ///     Instantiates a new ticket controller with the specified database model
        /// </summary>
        public TicketController(TheaterDbEntities db)
        {
            _db = db;
        }

        /// <summary>
        ///     Gets the purchase index page
        /// </summary>
        /// <returns>A view of the purchase page</returns>
        
        [HttpGet]
        public ActionResult Purchase(int showtimeId)
        {
            var showtime = _db.Showtimes.Where(s => s.Showtime_ID == showtimeId)
                .Include(s => s.Movie).SingleOrDefault();
            ViewBag.Showtime = showtime ?? throw new HttpException((int)HttpStatusCode.NotFound, "Showtime not found");

            return View(new PurchaseViewModel
            {
                ShowtimeId = showtimeId
            });
        }

        /// <summary>
        ///     Attempts to register the user specified in the model
        /// </summary>
        /// <param name="model">The purchase view model containing the purchase information</param>
        /// <returns>A view of the resulting confirmation page</returns>
        [HttpPost]
        [Authorize(Roles = "web.home.view")]
        [ValidateAntiForgeryToken]
        public ActionResult Index(PurchaseViewModel model)
        {
            var showtime = _db.Showtimes.Where(s => s.Showtime_ID == model.ShowtimeId)
                .Include(s => s.Movie).SingleOrDefault();
            ViewBag.Showtime = showtime ?? throw new HttpException((int)HttpStatusCode.NotFound, "Showtime not found");

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            return View(model);
        }
    }
}
