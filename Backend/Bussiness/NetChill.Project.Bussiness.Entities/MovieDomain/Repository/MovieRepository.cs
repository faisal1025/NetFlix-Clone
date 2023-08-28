using Microsoft.EntityFrameworkCore;
using NetChill.Project.DataAccess.Data.Dbcontext;
using NetChill.Project.DataAccess.Data.Repository;
using NetChill.Project.DataAccess.Domains.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NetChill.Project.Bussiness.Entities.MovieDomains.Repository
{
    public class MovieRepository : Repository<MovieDomain>, IMovieRepository
    {
        private readonly NetChillDbContext context;
        public MovieRepository(NetChillDbContext context) : base(context)
        {
            this.context = context;
        }
        public virtual MovieUser FindMovie(Expression<Func<MovieUser, bool>> predicate)
        {
            return context.Set<MovieUser>().FirstOrDefault(predicate);
        }
        public virtual void AddMovie(MovieUser movie)
        {
            context.Set<MovieUser>().Add(movie);
        }

        public async virtual Task<UserDomain> GetMovieUser(Expression<Func<UserDomain, bool>> predicate)
        {
            return await context.Set<UserDomain>().Include(x => x.Movies).ThenInclude(y => y.Movie).FirstOrDefaultAsync(predicate);
        }

        public async Task<IList<MovieDomain>> SearchMovie(string value)
        {
            return await context.Set<MovieDomain>().Where<MovieDomain>(x => x.Name.Contains(value)).ToListAsync<MovieDomain>();
        }
    }
}
