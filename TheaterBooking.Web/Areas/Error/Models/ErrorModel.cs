using System;
using System.Web;
using Castle.Core.Logging;
using JetBrains.Annotations;

namespace TheaterBooking.Web.Areas.Error.Models
{
    /// <summary>
    ///     View model for the error pages
    /// </summary>
    public class ErrorModel
    {
        /// <summary>
        ///     Initializes a new instance of the ErrorModel class, for the path where ASP.NET throws the error.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <param name="errorId">The error identifier</param>
        internal ErrorModel([NotNull] Exception exception, [NotNull] string errorId)
        {
            // Exception is not null if ASP.NET threw the error.
            // Exception is null if IIS throws the error
            Exception = exception;
            ErrorId = errorId;
        }

        /// <summary>
        ///     Initializes a new instance of the ErrorModel class, for the path where IIS handles the error.
        /// </summary>
        /// <param name="logger">The logger.</param>
        internal ErrorModel([NotNull] ILogger logger)
        {
            ErrorId = Guid.NewGuid().ToString();
            logger.Error($"IIS is handling a {HttpContext.Current.Request.RequestContext.RouteData.Values["action"]} " +
                         $"error; ErrorId = {ErrorId}");
        }

        /// <summary>
        ///     Gets the exception object
        /// </summary>
        [CanBeNull]
        public Exception Exception { get; }

        /// <summary>
        ///     Gets the unique error identifier associated with the error
        /// </summary>
        [NotNull]
        public string ErrorId { get; }
    }
}
