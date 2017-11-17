using System.Collections.Generic;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Newtonsoft.Json;

namespace TheaterBooking.Web.Users.Roles
{
    /// <summary>
    ///     Represents a role within the application
    /// </summary>
    public class Role : IdentityRole, IRole
    {
        /// <summary>
        ///     Gets the users in the role
        /// </summary>
        [JsonIgnore]
        public override ICollection<IdentityUserRole> Users => base.Users;
    }
}
