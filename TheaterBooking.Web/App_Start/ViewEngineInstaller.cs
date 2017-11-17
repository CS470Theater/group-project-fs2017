using TheaterBooking.Web;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using JetBrains.Annotations;
using System.Linq;
using System.Web.Mvc;

[assembly: AspMvcViewLocationFormat(ViewEngineInstaller.LocationFormat)]
[assembly: AspMvcMasterLocationFormat(ViewEngineInstaller.LocationFormat)]
[assembly: AspMvcPartialViewLocationFormat(ViewEngineInstaller.LocationFormat)]

namespace TheaterBooking.Web
{
    /// <summary>
    ///     Installs and configures view engines for the entire application
    /// </summary>
    [UsedImplicitly]
    public class ViewEngineInstaller : IWindsorInstaller
    {
        internal const string LocationFormat = "~/Areas/Shared/{0}.cshtml";

        /// <summary>
        ///     Performs the installation in the <see cref="T:Castle.Windsor.IWindsorContainer" />.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="store">The configuration store.</param>
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            foreach (var engine in ViewEngines.Engines.Where(engine => !(engine is RazorViewEngine)).ToList())
            {
                ViewEngines.Engines.Remove(engine);
            }

            foreach (var engine in ViewEngines.Engines.Cast<RazorViewEngine>())
            {
                engine.FileExtensions = new[] {"cshtml"};
                engine.AreaMasterLocationFormats = engine.AreaMasterLocationFormats
                    .Where(value => value.EndsWith("cshtml")).ToArray();
                engine.AreaPartialViewLocationFormats = engine.AreaMasterLocationFormats;
                engine.AreaViewLocationFormats = engine.AreaMasterLocationFormats;
                engine.MasterLocationFormats = new[] {LocationFormat};
                engine.PartialViewLocationFormats = engine.MasterLocationFormats;
                engine.ViewLocationFormats = engine.MasterLocationFormats;
            }
        }
    }
}
