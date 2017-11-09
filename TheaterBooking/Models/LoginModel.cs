using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TheaterBooking.Models
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

    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}