using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using JetBrains.Annotations;
using TheaterBooking.Web.Areas.Admin.Models;

namespace TheaterBooking.Web
{
    public class SqlInstaller : IWindsorInstaller
    {
        public void Install([NotNull] IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<TheaterDBEntities>().LifestyleTransient());
        }
    }
}
