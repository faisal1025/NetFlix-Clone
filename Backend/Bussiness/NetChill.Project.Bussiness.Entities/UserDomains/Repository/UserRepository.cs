using Microsoft.EntityFrameworkCore;
using NetChill.Project.DataAccess.Data.Dbcontext;
using NetChill.Project.DataAccess.Data.Repository;
using NetChill.Project.DataAccess.Domains.Domains;
using NetChill.Project.Foundation.Core.ValueObjects;
using NetChill.Project.UserDomains.AppServices.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NetChill.Project.Bussiness.Entities.UserDomains.Repository
{
    public class UserRepository : Repository<UserDomain>, IUserRepository
    {
        private readonly NetChillDbContext _context;
        public UserRepository(NetChillDbContext context) : base(context)
        {
            this._context = context;
        }

      
    }
}
