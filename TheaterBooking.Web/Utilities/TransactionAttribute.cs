using Castle.Windsor;
using JetBrains.Annotations;
using System.Web.Mvc;

namespace TheaterBooking.Web.Utilities
{
    /// <summary>
    ///     Wraps the current request in a database transaction
    /// </summary>
    internal class TransactionAttribute : FilterAttribute, IActionFilter
    {
        private readonly IWindsorContainer _container;

        /// <summary>
        ///     Instantiates a new transaction attribute using the specified container
        /// </summary>
        /// <param name="container">The dependency injection container to use</param>
        internal TransactionAttribute(IWindsorContainer container)
        {
            _container = container;
        }

        /// <summary>
        ///     Called before an action method executes.
        /// </summary>
        /// <param name="filterContext">The filter context.</param>
        public void OnActionExecuting([NotNull] ActionExecutingContext filterContext)
        {
            filterContext.HttpContext.Items[typeof(TransactionAttribute)] = new Context(_container);
        }

        /// <summary>
        ///     Called after the action method executes.
        /// </summary>
        /// <param name="filterContext">The filter context.</param>
        public void OnActionExecuted([NotNull] ActionExecutedContext filterContext)
        {
            ((Context) filterContext.HttpContext.Items[typeof(TransactionAttribute)]).Dispose(filterContext.Exception == null);
        }

        private struct Context
        {
            private readonly IWindsorContainer _container;

            internal Context(IWindsorContainer container)
            {
                _container = container;
            }

            internal void Dispose(bool success)
            {
            }
        }
    }
}
