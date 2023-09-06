using NetChill.Project.Bussiness.Entities.MovieDomains.AppServices.DTOs;
using NetChill.Project.DataAccess.Domains.Domains;
using NetChill.Project.Foundation.Core.ValueObjects;
using NetChill.Project.MovieDomains.AppServices.DTOs;
using NetChill.Project.UserDomains.AppServices.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NetChill.Project.MovieDomains.AppServices
{
    public interface IMovieAppService
    {
        OperationResult<MovieDTO> Create(MovieDTO item);
        OperationResult<IEnumerable<MovieDTO>> GetAllMovies();

        OperationResult<IEnumerable<MovieDTO>> GetUpcomingMovies();

        OperationResult<IEnumerable<MovieDTO>> GetNewReleaseMovies();

        OperationResult<IEnumerable<MovieDTO>> GetFeaturedMovies();

        OperationResult<MovieDTO> GetMovieByID(int id);
        bool Find(MovieDTO item);
        public OperationResult<MovieDTO> DeleteMovie(int id);

        OperationResult<MovieUserDTO> AddMovieToList(MovieUserDTO movieUser);

        Task<OperationResult<IList<MovieDomain>>> GetMovieList(int Id);

        Task<OperationResult<IList<MovieDTO>>> SearchedMovie(string value);

    }
}
