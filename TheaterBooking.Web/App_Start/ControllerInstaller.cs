using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using JetBrains.Annotations;
using System.Web.Mvc;
using System.Web.Routing;

namespace TheaterBooking.Web
{
    /// <summary>
    ///     Installs all of the MVC controllers in the application
    /// </summary>
    [UsedImplicitly]
    public class ControllerInstaller : IWindsorInstaller
    {
        /// <summary>
        ///     Performs the installation in the <see cref="T:Castle.Windsor.IWindsorContainer" />.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="store">The configuration store.</param>
        public void Install([NotNull] IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes.FromThisAssembly().BasedOn<IController>().LifestyleTransient());
            RouteTable.Routes.MapMvcAttributeRoutes();
        }
    }
}
