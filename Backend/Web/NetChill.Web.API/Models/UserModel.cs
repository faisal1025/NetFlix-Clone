using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NetChill.Web.API.Models
{
    public class UserModel
    {
        public int Id { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "Please enter your name.")]
        public string UserName { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Please enter your valid email.")]
        [EmailAddress]
        public string UserEmail { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Enter your password.")]
        [MinLength(8, ErrorMessage = "Please enter a password of 8 character.")]
        public string Password { get; set; }

        public string Role { get; set; } = "User";
    }
}
