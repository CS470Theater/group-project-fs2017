using JetBrains.Annotations;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;

namespace TheaterBooking.Web.Areas.Account.Controllers
{
    /// <summary>
    ///     Helper methods for handling identity result errors
    /// </summary>
    internal static class IdentityResultExtensions
    {
        /// <summary>
        ///     Checks if the identity result returned success, if not, prepares the ModelState to display the errors.
        /// </summary>
        /// <param name="identityResult">The identity result to validate</param>
        /// <param name="modelState">The model state to update if the result was unsuccessful</param>
        /// <returns>True if the identity result succeeded, otherwise false</returns>
        internal static bool IsValid([NotNull] this IdentityResult identityResult, ModelStateDictionary modelState)
        {
            if (identityResult.Succeeded)
            {
                return true;
            }

            foreach (var error in identityResult.Errors)
            {
                modelState.AddModelError("", error);
            }

            return false;
        }
    }
}
