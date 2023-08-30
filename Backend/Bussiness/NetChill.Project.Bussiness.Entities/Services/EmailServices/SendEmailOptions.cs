using NetChill.Project.Bussiness.Entities.UserDomains.AppServices.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetChill.Project.Bussiness.Entities.Services.EmailServices
{
    public class SendEmailOptions
    {
        public string Subject { get; set; }
        public IList<string> SendTo { get; set; }
        public string Body { get; set; }
    }
}
