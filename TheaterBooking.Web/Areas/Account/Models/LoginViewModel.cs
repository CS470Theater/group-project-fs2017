using System.ComponentModel.DataAnnotations;
using JetBrains.Annotations;

namespace TheaterBooking.Web.Areas.Account.Models
{
    /// <summary>
    ///     Represents a user's login information
    /// </summary>
    public class LoginViewModel
    {
        /// <summary>
        ///     Gets or sets the user's e-mail address
        /// </summary>
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        ///     Gets or sets the user's password
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [StringLength(1024, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 1)]
        public string Password { get; set; }

        /// <summary>
        ///     Gets or sets the cryptographic salt used to authenticate the redirect URI
        /// </summary>
        [CanBeNull]
        [StringLength(64)]
        public string Salt { get; set; }

        /// <summary>
        ///     Gets or sets the encrypted redirect URI
        /// </summary>
        [CanBeNull]
        [StringLength(1024)]
        public string RedirectUri { get; set; }
    }
}
