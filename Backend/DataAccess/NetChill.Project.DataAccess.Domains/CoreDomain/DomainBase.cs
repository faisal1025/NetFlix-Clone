using System;
using System.Collections.Generic;
using System.Text;

namespace NetChill.Project.DataAccess.Domains.CoreDomain
{
    public abstract class DomainBase: IDomain
    {
        public virtual int Id { get; set; }
    }
}
