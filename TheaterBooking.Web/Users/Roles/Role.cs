using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace TheaterBooking.Core.Users.Roles
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
