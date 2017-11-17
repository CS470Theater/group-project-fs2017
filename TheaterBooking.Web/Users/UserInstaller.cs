using TheaterBooking.Core.Users.Roles;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using JetBrains.Annotations;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace TheaterBooking.Core.Users
{
    /// <summary>
    ///     Installs ASP.NET identity management into the application
    /// </summary>
    [UsedImplicitly]
    public class IdentityInstaller : IWindsorInstaller
    {
        /// <summary>
        ///     Performs the installation in the <see cref="T:Castle.Windsor.IWindsorContainer" />.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="store">The configuration store.</param>
        public void Install([NotNull] IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<RoleManager<Role, string>>().LifestylePerWebRequest());
            container.Register(Component.For<DbContext>()
                .ImplementedBy<IdentityDbContext<User, Role, string, IdentityUserLogin, IdentityUserRole, IdentityUserClaim>>()
                .Named("identity")
                .LifestylePerWebRequest());
            container.Register(Component.For<IUserStore<User, string>>()
                .ImplementedBy<UserStore<User, Role, string, IdentityUserLogin, IdentityUserRole, IdentityUserClaim>>()
                .DependsOn(Dependency.OnComponent(typeof(DbContext), "identity"))
                .LifestylePerWebRequest());
            container.Register(Component.For<IRoleStore<Role, string>>()
                .ImplementedBy<RoleStore<Role, string, IdentityUserRole>>()
                .DependsOn(Dependency.OnComponent(typeof(DbContext), "identity"))
                .LifestylePerWebRequest());
        }
    }
}
