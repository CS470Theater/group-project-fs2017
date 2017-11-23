using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using JetBrains.Annotations;
using TheaterBooking.Web.Database;

namespace TheaterBooking.Web
{
    public class SqlInstaller : IWindsorInstaller
    {
        public void Install([NotNull] IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<TheaterDbEntities>().LifestyleTransient());
        }
    }
}
