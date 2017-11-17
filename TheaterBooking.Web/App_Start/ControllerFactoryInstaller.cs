using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using JetBrains.Annotations;
using System;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace TheaterBooking.Web
{
    /// <summary>
    ///     Sets up MVC Controllers to be created using dependency injection
    /// </summary>
    [UsedImplicitly]
    public class ControllerFactoryInstaller : IWindsorInstaller
    {
        /// <summary>
        ///     Performs the installation in the <see cref="T:Castle.Windsor.IWindsorContainer" />.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="store">The configuration store.</param>
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            ControllerBuilder.Current.SetControllerFactory(new ControllerFactory(container));
        }

        /// <summary>
        ///     A <see cref="IControllerFactory" /> implementation using Castle.Windsor for dependency injection.
        /// </summary>
        private class ControllerFactory : DefaultControllerFactory
        {
            private readonly IWindsorContainer _container;

            /// <summary>
            ///     Instantiates a new instance of ControllerFactory using the specified Castle.Windsor container.
            /// </summary>
            /// <param name="container">The container to use for controller dependency injection</param>
            public ControllerFactory(IWindsorContainer container)
            {
                _container = container;
            }

            /// <summary>
            ///     Retrieves the controller instance for the specified request context and controller type.
            /// </summary>
            /// <returns>The controller instance.</returns>
            /// <param name="requestContext">The context of the HTTP request, which includes the HTTP context and route data.</param>
            /// <param name="controllerType">The type of the controller.</param>
            /// <exception cref="T:System.Web.HttpException">
            ///     <paramref name="controllerType" /> is null.
            /// </exception>
            /// <exception cref="T:System.ArgumentException">
            ///     <paramref name="controllerType" /> cannot be assigned.
            /// </exception>
            /// <exception cref="T:System.InvalidOperationException">
            ///     An instance of <paramref name="controllerType" /> cannot be created.
            /// </exception>
            protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
            {
                if (controllerType == null)
                {
                    throw new HttpException((int)HttpStatusCode.NotFound,
                        $"The controller for '{requestContext.HttpContext.Request.Path}' could not be found.");
                }

                return (IController) _container.Resolve(controllerType);
            }

            /// <summary>
            ///     Releases the specified controller.
            /// </summary>
            /// <param name="controller">The controller to release.</param>
            public override void ReleaseController(IController controller)
            {
                _container.Release(controller);
            }
        }
    }
}
