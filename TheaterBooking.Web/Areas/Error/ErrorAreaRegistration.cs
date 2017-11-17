using System.Web.Mvc;

namespace TheaterBooking.Web.Areas.Error
{
    /// <summary>
    ///     Registers the Error area with ASP.NET
    /// </summary>
    public class ErrorAreaRegistration : AreaRegistration
    {
        /// <summary>
        ///     Gets the name of the area to register.
        /// </summary>
        /// <returns>The name of the area to register.</returns>
        public override string AreaName => "Error";

        /// <summary>
        ///     Registers an area in an ASP.NET MVC application using the specified area's context information.
        /// </summary>
        /// <param name="context">Encapsulates the information that is required in order to register the area.</param>
        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                name: "Error_Default",
                url: "Error/{action}",
                defaults: new { controller = "Error", area = "Error", action = "NotFound" }
            );
        }
    }
}