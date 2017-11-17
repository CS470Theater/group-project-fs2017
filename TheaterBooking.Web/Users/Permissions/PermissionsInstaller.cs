using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using JetBrains.Annotations;

namespace TheaterBooking.Web.Users.Permissions
{
    /// <summary>
    ///     Installs the permissions model into the application
    /// </summary>
    [UsedImplicitly]
    public class PermissionsInstaller : IWindsorInstaller
    {
        /// <summary>
        ///     Performs the installation in the <see cref="T:Castle.Windsor.IWindsorContainer" />.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="store">The configuration store.</param>
        public void Install([NotNull] IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IPermissionsModel>().ImplementedBy<PermissionsModel>().LifestyleSingleton());
        }
    }
}
