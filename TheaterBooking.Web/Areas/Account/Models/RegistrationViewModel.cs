using System.ComponentModel.DataAnnotations;

namespace TheaterBooking.Web.Areas.Account.Models
{
    /// <summary>
    ///     Represents a user's registration information
    /// </summary>
    public class RegistrationViewModel
    {
        /// <summary>
        ///     Gets or sets the user's e-mail address
        /// </summary>
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        /// <summary>
        ///     Gets or sets the user's password
        /// </summary>
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        /// <summary>
        ///     Gets or sets the user's password
        /// </summary>
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and the confirmation password did not match.")]
        public string ConfirmPassword { get; set; }
    }
}
