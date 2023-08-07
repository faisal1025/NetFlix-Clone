using NetChill.Project.DataAccess.Data.Dbcontext;
using NetChil.Project.Foundation.Core.ExceptionManagement;
using NetChill.Project.DataAccess.Data.UoW;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetChill.Project.Bussiness.Entities.UserDomains.UoW
{
    public class UserUnitOfWork : UnitOfWork, IUserUnitOfWork
    {
        /// <summary>
        /// The service provider
        /// </summary>
        //private readonly IServiceProvider ServiceProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="MyProjectUnitOfWork"/> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        /// <param name="serviceProvider">The service provider.</param>
        public UserUnitOfWork(NetChillDbContext dbContext, IExceptionManager exceptionManager)
            : base(dbContext, exceptionManager)
        {
            //ServiceProvider = serviceProvider;
        }
    }
}
