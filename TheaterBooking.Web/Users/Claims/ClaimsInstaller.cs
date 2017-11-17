using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using JetBrains.Annotations;
using Microsoft.AspNet.Identity;

namespace TheaterBooking.Core.Users.Claims
{
    /// <summary>
    ///     Installs a custom claims provider into the application
    /// </summary>
    [UsedImplicitly]
    public class ClaimsInstaller : IWindsorInstaller
    {
        /// <summary>
        ///     Performs the installation in the <see cref="T:Castle.Windsor.IWindsorContainer" />.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="store">The configuration store.</param>
        public void Install([NotNull] IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IClaimsIdentityFactory<User, string>>()
                .ImplementedBy<ClaimsIdentityFactory>().LifestylePerWebRequest());
        }
    }
}
