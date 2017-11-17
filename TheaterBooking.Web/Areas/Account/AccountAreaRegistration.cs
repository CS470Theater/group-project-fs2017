using System.Web.Mvc;
using JetBrains.Annotations;

namespace TheaterBooking.Web.Areas.Account
{
    /// <summary>
    ///     Registers the Account area with ASP.NET
    /// </summary>
    public class AccountAreaRegistration : AreaRegistration
    {
        /// <summary>
        ///     Gets the name of the area to register.
        /// </summary>
        /// <returns>The name of the area to register.</returns>
        public override string AreaName => "Account";

        /// <summary>
        ///     Registers an area in an ASP.NET MVC application using the specified area's context information.
        /// </summary>
        /// <param name="context">Encapsulates the information that is required in order to register the area.</param>
        public override void RegisterArea([NotNull] AreaRegistrationContext context)
        {
            context.MapRoute("Default", "", new { controller = "Home", action = "Index" });
            context.MapRoute("Account_default", "Account/{controller}/{action}", new {action = "Index"});
        }
    }
}
