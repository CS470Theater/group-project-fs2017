using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Castle.Core;
using Castle.DynamicProxy;
using Castle.MicroKernel;
using Castle.MicroKernel.ModelBuilder;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using JetBrains.Annotations;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace TheaterBooking.Web.Users.Claims
{
    /// <summary>
    ///     Updates the claims on the current user when they're changed.
    /// </summary>
    [UsedImplicitly]
    public class ClaimsSynchronizer : IInterceptor
    {
        private readonly Lazy<SignInManager<User, string>> _signinManager;

        /// <summary>
        ///     Instantiates a new claims synchronizer using the specified permissions model
        /// </summary>
        /// <param name="signinManager">The manager used to control application sign ins</param>
        public ClaimsSynchronizer(Lazy<SignInManager<User, string>> signinManager)
        {
            _signinManager = signinManager;
        }

        /// <summary>
        ///     Intercepts a call to a virtual method
        /// </summary>
        /// <param name="invocation">The invocation of the virtual method</param>
        public void Intercept([NotNull] IInvocation invocation)
        {
            invocation.Proceed();

            switch (invocation.Method.Name)
            {
                case nameof(UserManager<User, string>.RemoveFromRoleAsync):
                case nameof(UserManager<User, string>.RemoveFromRolesAsync):
                case nameof(UserManager<User, string>.AddToRoleAsync):
                case nameof(UserManager<User, string>.AddToRolesAsync):
                    if (!Equals(invocation.Arguments[0], ClaimsPrincipal.Current.Identity.GetUserId()))
                    {
                        return;
                    }
                    break;
                default:
                    return;
            }

            var userManager = (UserManager<User, string>) invocation.InvocationTarget;
            invocation.ReturnValue = RefreshToken(invocation.ReturnValue);
            async Task<IdentityResult> RefreshToken(object task)
            {
                var result = await (Task<IdentityResult>) task;
                _signinManager.Value.AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                var user = await userManager.FindByIdAsync((string) invocation.Arguments[0]);
                _signinManager.Value.AuthenticationManager.SignIn(new AuthenticationProperties {IsPersistent = true},
                    await userManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie));
                await _signinManager.Value.SignInAsync(user, true, false);
                return result;
            }
        }

        /// <summary>
        ///     Installs the claims synchronizer into the application
        /// </summary>
        [UsedImplicitly]
        public class Installer : IWindsorInstaller
        {
            /// <summary>
            ///     Performs the installation in the <see cref="T:Castle.Windsor.IWindsorContainer" />.
            /// </summary>
            /// <param name="container">The container.</param>
            /// <param name="store">The configuration store.</param>
            public void Install([NotNull] IWindsorContainer container, IConfigurationStore store)
            {
                container.Kernel.ComponentModelBuilder.AddContributor(new Contributor());
                container.Register(Component.For<ClaimsSynchronizer>().LifestylePerWebRequest());
                container.Register(Component.For<UserManager<User, string>>().LifestylePerWebRequest());
            }
        }

        /// <summary>
        ///     Contributes aspect oriented programming interceptors to the configuration of a component
        /// </summary>
        private class Contributor : IContributeComponentModelConstruction
        {
            /// <summary>
            ///     Usually the implementation will look in the configuration property of the model or the service interface, or the
            ///     implementation looking for something.
            /// </summary>
            /// <param name="kernel">The kernel instance</param>
            /// <param name="model">The component model</param>
            public void ProcessModel(IKernel kernel, [NotNull] ComponentModel model)
            {
                if (model.Services.Any(service => service == typeof(UserManager<User, string>)))
                {
                    model.Interceptors.AddIfNotInCollection(new InterceptorReference(typeof(ClaimsSynchronizer)));
                }
            }
        }
    }
}
