using Castle.Facilities.Startable;
using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;
using Castle.Windsor.Installer;
using System;
using System.Web;
using System.Web.Mvc;

namespace TheaterBooking.Web
{
    /// <summary>
    ///     Defines the implementation of methods common to all application objects in this ASP.NET application.
    /// </summary>
    public class MvcApplication : HttpApplication
    {
        private static IWindsorContainer _container;

        /// <summary>
        ///     Called when the first resource (such as a page) in an ASP.NET application is requested.
        /// </summary>
        /// <remarks>
        ///     The Application_Start method is called only one time during the life cycle of an application. You can use this
        ///     method to perform startup tasks such as loading data into the cache and initializing static values. You should set
        ///     only static data during application start. Do not set any instance data because it will be available only to the
        ///     first instance of the HttpApplication class that is created.
        /// </remarks>
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            _container = new WindsorContainer();
            _container.Kernel.Resolver.AddSubResolver(new ListResolver(_container.Kernel));
            _container.AddFacility<TypedFactoryFacility>();
            _container.AddFacility<StartableFacility>(factory => factory.DeferredStart());
            _container.Install(FromAssembly.InThisApplication());
        }

        /// <summary>
        ///     Called once per lifetime of the application before the application is unloaded.
        /// </summary>
        protected void Application_End(object sender, EventArgs e) => _container.Dispose();
    }
}
