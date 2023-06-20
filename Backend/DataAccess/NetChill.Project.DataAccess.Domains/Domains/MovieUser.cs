using NetChill.Project.DataAccess.Domains.CoreDomain;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetChill.Project.DataAccess.Domains.Domains
{
    public class MovieUser
    {
        public int MovieId { get; set; }
        public MovieDomain Movie { get; set; }
        public int UserId { get; set; }
        public UserDomain User { get; set; }
    }
}
