using System;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using TheaterBooking.Web.Areas.Error.Models;
using TheaterBooking.Web.Utilities;
using Castle.Core.Logging;
using Castle.Windsor;
using JetBrains.Annotations;

namespace TheaterBooking.Web.Areas.Error.Controllers
{
    /// <summary>
    ///     Provides error handling services for the application
    /// </summary>
    internal class ErrorAttribute : FilterAttribute, IExceptionFilter
    {
        private readonly IWindsorContainer _container;

        /// <summary>
        ///     Instantiates a new error attribute using the specified container for dependency injection
        /// </summary>
        /// <param name="container">The dependency injection container</param>
        public ErrorAttribute(IWindsorContainer container)
        {
            _container = container;
        }

        /// <summary>Called when an exception occurs.</summary>
        /// <param name="context">The filter context.</param>
        public void OnException([NotNull] ExceptionContext context)
        {
            if (context.IsChildAction || context.ExceptionHandled)
            {
                return;
            }

            // Implements the custom error pages for errors **from within the ASP.NET framework**.
            // Errors from IIS are handled using httpErrors in the web config.
            // Errors come from IIS if you return an action result with a status code
            // Errors come from ASP.NET if you throw HttpException.
            // Throw HttpException if you want a non-default message associated with the error

            // Again, errors from IIS do NOT get executed here

            var errorId = Guid.NewGuid().ToString();
            var logger = _container.Resolve<ILogger>();
            logger.Error(errorId, context.Exception);
            _container.Release(logger);

            if (context.HttpContext.Response.HeadersWritten)
            {
                // It's too late to change the response.
                return;
            }

            if (context.HttpContext.Request.AcceptTypes?.Any(type => type == "application/json") == true ||
                context.HttpContext.Request.Headers["x-requested-with"] == "XMLHttpRequest")
            {
                // REST API error
                if (ClaimsPrincipal.Current.IsInRole("web.admin.view"))
                {
                    // User is an admin, show them the juicy tidbits
                    context.Result = new JsonContentResult(new
                    {
                        context.Exception.Message,
                        context.Exception.Data,
                        context.Exception.GetType().FullName,
                        ErrorId = errorId,
                        context.Exception.StackTrace,
                        InnerException = context.Exception.InnerException != null
                            ? new
                            {
                                context.Exception.InnerException.Message,
                                context.Exception.InnerException.Data,
                                context.Exception.InnerException.GetType().FullName,
                                context.Exception.InnerException.StackTrace
                            }
                            : null
                    });
                }
                else
                {
                    // User is not an admin, don't show them any juicy nuggets
                    context.Result = new JsonContentResult(new
                    {
                        context.Exception.Message,
                        ErrorId = errorId
                    });
                }
            }
            else
            {
                var view = ErrorController.GetErrorView(context.Exception);
                if (view == null)
                {
                    // no specific view for this error
                    return;
                }

                // Request is for an HTML page, render the appropriate view
                context.Result = new ViewResult
                {
                    ViewName = $"~/Areas/Error/Views/Error/{view}.cshtml",
                    ViewData = new ViewDataDictionary<ErrorModel>(new ErrorModel(context.Exception, errorId)),
                    TempData = context.Controller.TempData
                };
            }

            context.ExceptionHandled = true;

            // Based on System.Web.Mvc.HandleErrorAttribute
            var httpException = context.Exception as HttpException;
            var response = context.HttpContext.Response;
            response.Clear();
            response.StatusCode = httpException?.GetHttpCode() ?? 500;
            response.StatusDescription = httpException?.Message;
            response.TrySkipIisCustomErrors = true;
        }
    }
}
