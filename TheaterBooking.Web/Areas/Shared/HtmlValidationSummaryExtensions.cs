using JetBrains.Annotations;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace TheaterBooking.Web.Areas.Shared
{
    /// <summary>
    ///     Extension methods for HTML validation summaries
    /// </summary>
    public static class HtmlValidationSummaryExtensions
    {
        /// <summary>
        ///     Renders a validation summary of all model-level errors
        /// </summary>
        /// <param name="html">The html helper to use</param>
        /// <returns>A validation summary, or null if no model-level errors exist</returns>
        [CanBeNull]
        public static IHtmlString ValidationSummaryForm(this HtmlHelper html)
        {
            return html.ViewData.ModelState.ContainsKey("")
                ? html.ValidationSummary(true, "", new {@class = "text-danger"})
                : null;
        }
    }
}
