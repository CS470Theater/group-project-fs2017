using System.Web.Mvc;

namespace TheaterBooking.Web.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName => "Admin";

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute("Admin_default", "Admin/{controller}/{action}", new {action = "Index"});
        }
    }
}