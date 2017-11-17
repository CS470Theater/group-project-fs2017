using JetBrains.Annotations;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace TheaterBooking.Web.Utilities
{
    /// <summary>
    ///     Transfers the control over the current request to another action method
    /// </summary>
    internal class TransferResult : ActionResult
    {
        private readonly string _action;
        private readonly string _controller;
        private readonly object _routeValues;

        /// <summary>
        ///     Instantiates a new transfer action result to transfer control over to the specified action method
        /// </summary>
        /// <param name="action">The action method to transfer control to</param>
        /// <param name="controller">The controller to transfer control to</param>
        /// <param name="routeValues">Additional parameters for the route to transfer control to</param>
        public TransferResult([AspMvcAction] string action, [AspMvcController] string controller = null,
            [AspMvcAction("Action")] [AspMvcArea("Area")] [AspMvcController("Controller")] object routeValues = null)
        {
            _action = action;
            _controller = controller;
            _routeValues = routeValues;
        }

        /// <summary>
        ///     Enables processing of the result of an action method by a custom type that inherits from the
        ///     <see cref="T:System.Web.Mvc.ActionResult" /> class.
        /// </summary>
        /// <param name="context">
        ///     The context in which the result is executed. The context information includes the controller,
        ///     HTTP content, request context, and route data.
        /// </param>
        public override void ExecuteResult(ControllerContext context)
        {
            var path = new UrlHelper(context.RequestContext).Action(_action, _controller, _routeValues);
            if (path == null)
            {
                throw new HttpException((int) HttpStatusCode.NotFound, $"URL to {_action} was not found");
            }

            context.HttpContext.Server.TransferRequest(path);
        }
    }
}
