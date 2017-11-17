using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using JetBrains.Annotations;

namespace TheaterBooking.Web
{
    /// <summary>
    ///     Allows Lazy components to be injected
    /// </summary>
    [UsedImplicitly]
    public class LazyInstaller : IWindsorInstaller
    {
        /// <summary>
        ///     Performs the installation in the <see cref="T:Castle.Windsor.IWindsorContainer" />.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="store">The configuration store.</param>
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<ILazyComponentLoader>()
                .ImplementedBy<LazyOfTComponentLoader>().LifestyleTransient());
        }
    }
}
