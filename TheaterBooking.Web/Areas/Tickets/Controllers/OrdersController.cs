using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Web.Mvc;
using TheaterBooking.Web.Database;

namespace TheaterBooking.Web.Areas.Tickets.Controllers
{
    public class OrdersController : Controller
    {
        private readonly TheaterDbEntities _db;

        public OrdersController(TheaterDbEntities db)
        {
            _db = db;
        }

        /// <summary>
        ///     Gets the orders index page
        /// </summary>
        /// <returns>A view of the purchase page</returns>
        [HttpGet]
        [Authorize(Roles = "web.home.view")]
        public ActionResult Index()
        {
            var customerId = ((ClaimsIdentity) User.Identity).FindFirst(ClaimTypes.NameIdentifier).Value;
            return View(_db.Bookings.Include(booking => booking.Tickets.Select(ticket => ticket.Price))
                .Include(booking => booking.Tickets.Select(ticket => ticket.Showtime))
                .Include(booking => booking.Tickets.Select(ticket => ticket.Showtime.Movie))
                .Where(booking => booking.Customer_ID == customerId)
                .OrderBy(booking => booking.Booking_Date)
                .ThenBy(booking => booking.Booking_ID)
                .ToList());
        }
    }
}
