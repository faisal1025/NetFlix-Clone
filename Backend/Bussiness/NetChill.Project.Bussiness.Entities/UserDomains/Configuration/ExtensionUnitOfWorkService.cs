using Microsoft.Extensions.DependencyInjection;
using NetChill.Project.Bussiness.Entities.UserDomains.Repository;
using NetChill.Project.Bussiness.Entities.UserDomains.UoW;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetChill.Project.UserDomains.Configuration
{ /// <summary>
  /// Unit of work service
  /// </summary>
    public static class ExtensionUnitOfWorkService
    {
        /// <summary>
        /// Registers the repositories.
        /// </summary>
        /// <param name="service">The service collection.</param>
		public static void RegisterRepositories1(this IServiceCollection service)
        {
            service.AddSingleton<IUserRepository, UserRepository>();
            service.AddSingleton<IUserUnitOfWork, UserUnitOfWork>();
        }
    }
}
