using System;
using System.Web;
using System.Web.Mvc;
using TheaterBooking.Web.Areas.Error.Models;
using TheaterBooking.Web.Utilities;
using Castle.Core.Logging;
using JetBrains.Annotations;

namespace TheaterBooking.Web.Areas.Error.Controllers
{
    [AllowAnonymous]
    public class ErrorController : Controller
    {
        [UsedImplicitly] // Dependency injection
        public ILogger Logger { get; set; } = NullLogger.Instance;

        // Action methods for when IIS wants to render the error pages.
        // ASP.NET errors go through ErrorAttribute
        public ActionResult BadRequest() => View();

        public ActionResult Unauthorized()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View();
            }

            // Encryption prevents malicious redirects
            var crypto = new ExternalCrypto();
            var queryString = Request.Url?.Query;
            return new TransferResult("Index", "Login", new
            {
                area = "Account",
                salt = crypto.Salt,
                redirectUri = queryString != null && Request.IsLocal
                    ? crypto.Encrypt(queryString.Substring(queryString.IndexOf(";", StringComparison.InvariantCulture) + 1))
                    : null
            });
        }

        public ActionResult Forbidden() => View();

        public ActionResult NotFound() => View();

        public ActionResult MethodNotAllowed() => View();
        
        public ActionResult Gone() => View();

        public ActionResult PayloadTooLarge() => View();

        public ActionResult UriTooLong() => View();

        public ActionResult TooManyRequests() => View();

        public ActionResult InternalError() => View(new ErrorModel(Logger));

        public ActionResult ServiceUnavailable() => View();

        [CanBeNull]
        internal static string GetErrorView([CanBeNull] Exception lastError)
        {
            switch ((lastError as HttpException)?.GetHttpCode())
            {
                case 400:
                    return "BadRequest";
                case 401:
                    return "Unauthorized";
                case 403:
                    return "Forbidden";
                case 404:
                    return "NotFound";
                case 405:
                    return "MethodNotAllowed";
                case 410:
                    return "Gone";
                case 413:
                    return "PayloadTooLarge";
                case 414:
                    return "UriTooLong";
                case 429:
                    return "TooManyRequests";
                case 500:
                case null:
                    return "InternalError";
                case 503:
                    return "ServiceUnavailable";
                default:
                    return null;
            }
        }
    }
}
