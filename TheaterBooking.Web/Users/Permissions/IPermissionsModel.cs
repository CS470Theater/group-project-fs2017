using System.Collections.Generic;

namespace TheaterBooking.Web.Users.Permissions
{
    /// <summary>
    ///     Model for permissions
    /// </summary>
    public interface IPermissionsModel
    {
        /// <summary>
        ///     Gets the permissions for the specified role
        /// </summary>
        /// <param name="role">The role to get the permissions of</param>
        /// <returns>The permissions for the specified role</returns>
        IEnumerable<string> this[string role] { get; }

        /// <summary>
        ///     Gets the available roles within the application
        /// </summary>
        IEnumerable<string> Roles { get; }
    }
}
