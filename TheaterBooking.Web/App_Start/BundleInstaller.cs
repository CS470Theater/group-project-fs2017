using System.Diagnostics;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using JetBrains.Annotations;
using System.Web.Optimization;

namespace TheaterBooking.Web
{
    /// <summary>
    ///     Installs bundles of scripts and stylesheets for the entire web application
    /// </summary>
    [UsedImplicitly]
    public class BundleInstaller : IWindsorInstaller
    {
        /// <summary>
        ///     Performs the installation in the <see cref="T:Castle.Windsor.IWindsorContainer" />.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="store">The configuration store.</param>
        public void Install([NotNull] IWindsorContainer container, IConfigurationStore store)
        {
#if !DEBUG
            BundleTable.EnableOptimizations = true;
#endif

            var bundles = BundleTable.Bundles;
            bundles.Add(new ScriptBundle("~/Content/scripts")
                    .Include("~/Scripts/jquery-{version}.js")
                    .Include("~/Scripts/jquery.browser.mobile.js")
                    .Include($"~/Scripts/react/react{(Debugger.IsAttached ? "" : ".min")}.js")
                    .Include($"~/Scripts/react/react-dom{(Debugger.IsAttached ? "" : ".min")}.js"));
            
            bundles.Add(new LessBundle("~/Content/css").Include("~/Content/*.less"));

            // Per-area bundles are registered in AreaInstaller
        }
    }
}
