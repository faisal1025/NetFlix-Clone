using NetChill.Project.DataAccess.Domains.Domains;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetChill.Project.Bussiness.Entities.MovieDomains.AppServices.DTOs
{
    public class MovieUserDTO
    {
        
        public int MovieId { get; set; }
        public MovieDomain Movie { get; set; }
        public int UserId { get; set; }
        public UserDomain User { get; set; }
    }
}
