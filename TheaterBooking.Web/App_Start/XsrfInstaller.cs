using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using JetBrains.Annotations;
using System.Web.Helpers;

namespace TheaterBooking.Web
{
    /// <summary>
    ///     Configures XSRF tokens
    /// </summary>
    [UsedImplicitly]
    public class XsrfInstaller : IWindsorInstaller
    {
        /// <summary>
        ///     Performs the installation in the <see cref="T:Castle.Windsor.IWindsorContainer" />.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="store">The configuration store.</param>
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            // Fixes an issue when loading several thousand transactions in a page;
            // http.sys kills the connection if the response headers exceed 64 KB or so
            AntiForgeryConfig.SuppressXFrameOptionsHeader = true;
        }
    }
}
