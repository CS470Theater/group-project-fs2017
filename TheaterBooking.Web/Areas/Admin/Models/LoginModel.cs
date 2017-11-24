using System.ComponentModel.DataAnnotations;

namespace TheaterBooking.Web.Areas.Admin.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Please enter user name.")]
        [DataType(DataType.Text)]
        [Display(Name = "Username")]
        [StringLength(30)]
        public string Username { get; set; }

        [Required(ErrorMessage = "Please enter password.")]
        [DataType(DataType.Text)]
        [Display(Name = "Password")]
        [StringLength(10)]
        public string Password { get; set; }
    }
}
