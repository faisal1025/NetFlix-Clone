using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using NetChill.Project.DataAccess.Data.Repository;
using NetChill.Project.DataAccess.Domains.Domains;
using NetChill.Project.Foundation.Core.ValueObjects;
using NetChill.Project.UserDomains.AppServices.DTOs;

namespace NetChill.Project.Bussiness.Entities.UserDomains.Repository
{
    public interface IUserRepository : IRepository<UserDomain>
    {

    }
}
