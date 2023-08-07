using Microsoft.Extensions.DependencyInjection;
using NetChill.Project.Bussiness.Entities.MovieDomains.Repository;
using NetChill.Project.Bussiness.Entities.MovieDomains.UoW;
using NetChill.Project.Bussiness.Entities.UserDomains.Repository;
using NetChill.Project.Bussiness.Entities.UserDomains.UoW;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetChill.Project.MovieDomains.Configuration
{ /// <summary>
  /// Unit of work service
  /// </summary>
    public static class ExtensionUnitOfWorkService
    {
        /// <summary>
        /// Registers the repositories.
        /// </summary>
        /// <param name="service">The service collection.</param>
		public static void RegisterRepositories(this IServiceCollection service)
        {
            service.AddSingleton<IMovieRepository, MovieRepository>();
            service.AddSingleton<IMovieUnitOfWork, MovieUnitOfWork>();
        }
    }
}
