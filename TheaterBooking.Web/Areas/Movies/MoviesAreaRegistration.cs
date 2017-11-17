using System.Web.Mvc;
using JetBrains.Annotations;

namespace TheaterBooking.Web.Areas.Movies
{
    public class MoviesAreaRegistration : AreaRegistration 
    {
        public override string AreaName { get; } = "Movies";

        public override void RegisterArea([NotNull] AreaRegistrationContext context) 
        {
            context.MapRoute("Movies_default", "Movies/{controller}/{action}", new { action = "Index" });
        }
    }
}
