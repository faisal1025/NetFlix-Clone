
using Microsoft.AspNetCore.Http;
using NetChill.Project.Bussiness.Entities.MovieDomains.AppServices.DTOs;
using NetChill.Project.DataAccess.Domains.Domains;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NetChill.Project.MovieDomains.AppServices.DTOs
{
    public class MovieDTO
    {
        public int Id { get; set; }
        /// <summary>
        /// Gets or sets the name of the product.
        /// </summary>
        /// <value>
        /// The name of the product.
        /// </value>
        public string Name { get; set; }
        public string Category { get; set; }
        public DateTime YoR { get; set; }
        public DateTime Starts { get; set; }
        public string Description { get; set; }
        public bool IsFeatured { get; set; }
        public string ImageName { get; set; }
        public string VideoName { get; set; }

   
        public IFormFile MoviePoster { get; set; }
 
        public IFormFile ContentPath { get; set; }

        public ICollection<MovieUserDTO> Users { get; set; }

    }
}
