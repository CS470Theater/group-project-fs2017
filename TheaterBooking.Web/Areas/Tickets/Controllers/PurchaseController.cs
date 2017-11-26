using Braintree;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TheaterBooking.Web.Areas.Tickets.Models;
using TheaterBooking.Web.Database;
using TheaterBooking.Web.Utilities;

namespace TheaterBooking.Web.Areas.Tickets.Controllers
{
    public class PurchaseController : Controller
    {
        private readonly TheaterDbEntities _db;

        private static readonly string MerchantId = new ExternalCrypto("Siba2KwbLDJxvKipKXJ3UQ")
            .Decrypt("ceeUgBOH1y7ygxC-MdTKvy-LqxzyaPn5FLZXOiToq5rYh8DNj06HS94e9x2HucU1OE6-UH4" +
            "5Q37hMYs_x247fgwURPskPYDEHGt4yx3LZq8giSntU-5gLi6MGC6tjCf5Z-CEhS_-Qke2DOvQmy_DDJ0");

        private static readonly string PublicKey = new ExternalCrypto("Ddz_GG7t8wj6VOpu2BYJxQ")
            .Decrypt("65WkvYTmhYS-shPTgCBisRM-9gNuZBjWiBk5MwR_wD3dbQpZ88mbmOKafuKG2jsuZESnWvb" +
            "rwn7KvGPEeCKkXwPT6dM4siLeSlzsrXFGbxbQFGBytXe1GiiquTP17dN_TKxu1b44aSvZV-W-jiHdB2c");

        private static readonly string PrivateKey = new ExternalCrypto("oDfGoUU95JFWSPYJRtYpBA")
            .Decrypt("REUrmG16hBBkPEO5H0Y1jmGE01lY9zWvSPBJo_E_YHcxsuYbF1TcDQ8JOayti5Y-8sDiT0mIONA2lEREY" +
            "w7MUgce4Yc95Kq4lQPi-pRtLcjggz4_32uKSHBDWI21b-ZS3lSwmv_5RZvTipaK2n3vDY68jkqPwzIacNBKnD8ERIwr");

        private readonly BraintreeGateway gateway = new BraintreeGateway()
        {
            Environment = Braintree.Environment.SANDBOX,
            MerchantId = MerchantId,
            PublicKey = PublicKey,
            PrivateKey = PrivateKey
        };

        /// <summary>
        ///     Instantiates a new purchase controller with the specified database model
        /// </summary>
        public PurchaseController(TheaterDbEntities db)
        {
            _db = db;
        }

        /// <summary>
        ///     Gets the purchase index page
        /// </summary>
        /// <returns>A view of the purchase page</returns>
        [HttpGet]
        [Authorize(Roles = "web.home.view")]
        public async Task<ActionResult> Index(int showtimeId)
        {
            var showtime = _db.Showtimes.Where(s => s.Showtime_ID == showtimeId)
                .Include(s => s.Movie).SingleOrDefault();
            ViewBag.Showtime = showtime ?? throw new HttpException((int)HttpStatusCode.NotFound, "Showtime not found");
            ViewBag.ClientToken = await gateway.ClientToken.GenerateAsync();

            return View(new PurchaseViewModel
            {
                ShowtimeId = showtimeId,
                Prices = _db.Prices.ToDictionary(price => price.Price_ID),
                Quantities = _db.Prices.ToDictionary(price => price.Price_ID, price => price.Price_ID == 1 ? 1U : 0)
            });
        }

        /// <summary>
        ///     Attempts to purchase the tickets specified in the model
        /// </summary>
        /// <param name="model">The purchase view model containing the purchase information</param>
        /// <returns>A view of the resulting confirmation page</returns>
        [HttpPost]
        [Authorize(Roles = "web.home.view")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Checkout(PurchaseViewModel model)
        {
            var showtime = _db.Showtimes.Where(s => s.Showtime_ID == model.ShowtimeId)
                .Include(s => s.Movie).SingleOrDefault();
            model.Prices = _db.Prices.ToDictionary(price => price.Price_ID);
            model.Quantities = model.Prices.ToDictionary(price => price.Key, price => uint.Parse(Request.Form.Get($"Quantities[{price.Key}]") ?? "0"));
            ViewBag.Showtime = showtime ?? throw new HttpException((int)HttpStatusCode.NotFound, "Showtime not found");
            ViewBag.ClientToken = await gateway.ClientToken.GenerateAsync();

            if (model.Quantities.Sum(qty => qty.Value) >= 20)
            {
                ModelState.AddModelError("", "Limit of 20 tickets per purchase");
            }

            if (!ModelState.IsValid)
            {
                return View("Index", model);
            }


            var amount = model.Quantities.Sum(qty => model.Prices[qty.Key].Price_Amount * qty.Value);
            var result = await gateway.Transaction.SaleAsync(new TransactionRequest {
                Amount = amount,
                PaymentMethodNonce = model.Nonce,
                Options = new TransactionOptionsRequest
                {
                    SubmitForSettlement = true,
                    PayPal = new TransactionOptionsPayPalRequest
                    {
                        Description = $"{model.Quantities.Sum(qty => qty.Value)} tickets for {showtime.Movie.Movie_Name}"
                    }
                }
            });

            if (!(result.IsSuccess()))
            {
                ModelState.AddModelError("", result.Message);
                return View("Index", model);
            }

            var booking = new Booking {
                Booking_Date = DateTime.Today,
                Customer_ID = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier).Value,
                Total_Cost = amount
            };

            foreach(var quantity in model.Quantities)
            {
                for(var i = 0; i != quantity.Value; i++)
                {
                    booking.Tickets.Add(new Ticket
                    {
                        Price = model.Prices[quantity.Key],
                        Showtime = showtime,
                        Booking = booking
                    });
                }
            }

            _db.Bookings.Add(booking);
            await _db.SaveChangesAsync();

            return View(model);
        }
    }
}
