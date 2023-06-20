
using NetChill.Project.Bussiness.Entities.MovieDomains.AppServices.DTOs;
using NetChill.Project.DataAccess.Domains.Domains;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetChill.Project.UserDomains.AppServices.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }
        /// <summary>
        /// Gets or sets the name of the product.
        /// </summary>
        /// <value>
        /// The name of the product.
        /// </value>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the product code.
        /// </summary>
        /// <value>
        /// The product code.
        /// </value>
        public string UserEmail { get; set; }
        public string Password { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordKey { get; set; }

        public string Role { get; set; } = "User";

        public ICollection<MovieUserDTO> Movies { get; set; }

    }
}
