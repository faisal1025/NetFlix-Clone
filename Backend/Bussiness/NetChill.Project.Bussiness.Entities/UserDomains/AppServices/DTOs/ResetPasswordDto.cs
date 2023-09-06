using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetChill.Project.Bussiness.Entities.UserDomains.AppServices.DTOs
{
    public class ResetPasswordDto
    {
        public string Password { get; set; }
        public string Confirmpassword { get; set; }
        public string Uid { get; set; }
    }
}
