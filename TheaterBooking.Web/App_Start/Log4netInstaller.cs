using Castle.Facilities.Logging;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Services.Logging.Log4netIntegration;
using Castle.Windsor;
using JetBrains.Annotations;

namespace TheaterBooking.Web
{
    /// <summary>
    ///     Installs log4net logging into the application.
    /// </summary>
    [UsedImplicitly]
    public class Log4NetInstaller : IWindsorInstaller
    {
        /// <summary>
        ///     Performs the installation in the <see cref="T:Castle.Windsor.IWindsorContainer" />.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="store">The configuration store.</param>
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.AddFacility<LoggingFacility>(facility => facility
                .LogUsing<Log4netFactory>().WithConfig("log4net.xml").ToLog("RollingLogFileAppender"));
        }
    }
}
