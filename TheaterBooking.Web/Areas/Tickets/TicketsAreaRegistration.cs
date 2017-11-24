using System.Web.Mvc;
using JetBrains.Annotations;

namespace TheaterBooking.Web.Areas.Tickets
{
    public class TicketsAreaRegistration : AreaRegistration 
    {
        public override string AreaName => "Tickets";

        public override void RegisterArea([NotNull] AreaRegistrationContext context) 
        {
            context.MapRoute("Tickets_default", "Tickets/{controller}/{action}", new { action = "Index" });
        }
    }
}
