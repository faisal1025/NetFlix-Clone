using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using NetChill.Project.DataAccess.Data.Repository;
using NetChill.Project.DataAccess.Domains.Domains;

namespace NetChill.Project.Bussiness.Entities.UserDomains.Repository
{
    public interface IUserRepository: IRepository<UserDomain>
    {
        
    }
}
