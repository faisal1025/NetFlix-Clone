using NetChill.Project.DataAccess.Domains.CoreDomain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NetChill.Project.DataAccess.Domains.Domains
{
    public class UserDomain : DomainBase
    {

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
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordKey { get; set; }

        public string Role { get; set; } = "User";

        public ICollection<MovieUser> Movies { get; set;}
    }

}
