using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using NetChill.Project.DataAccess.Data.Repository;
using NetChill.Project.DataAccess.Domains.Domains;

namespace NetChill.Project.Bussiness.Entities.MovieDomains.Repository
{
    public interface IMovieRepository: IRepository<MovieDomain>
    {
        void AddMovie(MovieUser movie);
        MovieUser FindMovie(Expression<Func<MovieUser, bool>> predicate);

        Task<UserDomain> GetMovieUser(Expression<Func<UserDomain, bool>> predicate);
    }
}
