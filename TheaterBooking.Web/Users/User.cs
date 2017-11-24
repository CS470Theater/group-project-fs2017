using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNet.Identity.EntityFramework;

namespace TheaterBooking.Web.Users
{
    /// <summary>
    ///     Represents a user and their profile
    /// </summary>
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        
        public string LastName { get; set; }

        [NotMapped]
        public string Name => $"{FirstName} {LastName}";
    }
}
