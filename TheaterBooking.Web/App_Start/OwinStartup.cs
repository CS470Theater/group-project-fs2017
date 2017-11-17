using TheaterBooking.Core.Users;
using TheaterBooking.Web;
using JetBrains.Annotations;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Owin;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Microsoft.Owin.Security;

[assembly: OwinStartup(typeof(OwinStartup))]

namespace TheaterBooking.Web
{
    /// <summary>
    ///     Sets up the OWIN container at startup
    /// </summary>
    public class OwinStartup
    {
        /// <summary>
        ///     Configures the specified OWIN application
        /// </summary>
        /// <param name="app">The OWIN application builder to configure</param>
        [UsedImplicitly]
        public void Configuration(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                Provider = new CookieAuthenticationProvider
                {
                    OnValidateIdentity =
                        SecurityStampValidator.OnValidateIdentity<UserManager<User>, User>(
                            TimeSpan.FromMinutes(30),
                            async (manager, user) =>
                                await
                                    manager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie))
                },
                CookieSecure = CookieSecureOption.Always
            });
            app.UseExternalSignInCookie();

            // to get your own: https://console.developers.google.com/
            var googleOptions = new GoogleOAuth2AuthenticationOptions
            {
                ClientId = @"104893415212-9j62mjqego1m0s8d3od76hr7m88rrk3r.apps.googleusercontent.com",
                ClientSecret = "3VxTRIpMaN5Xej_W4EmYNMdl",
                Scope = { "email", "profile", "https://www.googleapis.com/auth/plus.me" },
                Provider = new GoogleOAuth2AuthenticationProvider
                {
                    OnAuthenticated = context =>
                    {
                        foreach (var token in context.User)
                        {
                            context.Identity.AddClaim(new Claim(token.Key, token.Value.ToString()));
                        }

                        return Task.FromResult(context);
                    }
                }
            };
            app.UseGoogleAuthentication(googleOptions);
        }
        
        /// <summary>
        ///     Installs ASP.NET identity management into the application's OWIN context
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
                container.Register(Component.For<SignInManager<User, string>>().LifestylePerWebRequest());
                container.Register(Component.For<IAuthenticationManager>()
                    .UsingFactoryMethod(() => HttpContext.Current.GetOwinContext().Authentication)
                    .LifestylePerWebRequest());
                container.Register(Component.For<IOwinContext>()
                    .UsingFactoryMethod(() => HttpContext.Current.GetOwinContext())
                    .LifestylePerWebRequest());
            }
        }
    }
}
