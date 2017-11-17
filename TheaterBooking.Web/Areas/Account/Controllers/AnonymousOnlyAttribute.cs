using System.Web;
using System.Web.Mvc;
using JetBrains.Annotations;

namespace TheaterBooking.Web.Areas.Account.Controllers
{
    /// <summary>
    ///     Allows only unauthenticated users access to the action method.
    /// </summary>
    internal class AnonymousOnlyAttribute : AuthorizeAttribute
    {
        /// <summary>
        ///     When overridden, provides an entry point for custom authorization checks.
        /// </summary>
        /// <returns>true if the user is authorized; otherwise, false.</returns>
        /// <param name="context">
        ///     The HTTP context, which encapsulates all HTTP-specific information about an individual HTTP
        ///     request.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">The <paramref name="context" /> parameter is null.</exception>
        protected override bool AuthorizeCore([NotNull] HttpContextBase context) => !context.User.Identity.IsAuthenticated;
    }
}
