using Microsoft.AspNetCore.Http;
using NetChill.Project.DataAccess.Domains.CoreDomain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NetChill.Project.DataAccess.Domains.Domains
{
    public class MovieDomain:DomainBase
    {

        public string Name { get; set; }

        public string Category { get; set; }

      
        public DateTime YoR { get; set; }

        public DateTime Starts { get; set; }

        public string Description { get; set; }

        public bool IsFeatured { get; set; }
        public string ImageName { get; set; }
        public string VideoName { get; set; }

        public ICollection<MovieUser> Users { get; set; }
    }
}
