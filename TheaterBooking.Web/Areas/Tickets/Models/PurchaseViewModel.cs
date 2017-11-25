using System.Collections.Generic;
using TheaterBooking.Web.Database;

namespace TheaterBooking.Web.Areas.Tickets.Models
{
    public class PurchaseViewModel
    {
        public IDictionary<int, Price> Prices { get; set; }

        public IDictionary<int, uint> Quantities { get; set; }

        public int ShowtimeId { get; set; }

        public string Nonce { get; set; }
    }
}
