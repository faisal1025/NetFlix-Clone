using AutoMapper;
using Company.Project.Core.ExceptionManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using NetChill.Project.Bussiness.Entities.UserDomains.UoW;
using NetChill.Project.Bussiness.Entities.UserDomains.Repository;
using NetChil.Project.Foundation.Core.ExceptionManagement;
using NetChill.Project.UserDomains.AppServices.DTOs;
using NetChill.Project.Foundation.Core.ValueObjects;
using NetChill.Project.DataAccess.Domains.Domains;
using NetChill.Project.DataAccess.Data.UoW;
using NetChill.Project.MovieDomains.AppServices.DTOs;
using NetChill.Project.Bussiness.Entities.MovieDomains.UoW;
using NetChill.Project.Bussiness.Entities.MovieDomains.Repository;
using System.Threading.Tasks;
using NetChill.Project.Bussiness.Entities.MovieDomains.AppServices.DTOs;

namespace NetChill.Project.MovieDomains.AppServices
{
    public class MovieAppService : IMovieAppService
    {
        private IMapper mapper;
        private IMovieUnitOfWork unitOfWork;
        //private IExceptionManager exceptionManager;
        private IMovieRepository UserRepository;
        private readonly IExceptionManager exceptionManager;

        public MovieAppService(IMovieUnitOfWork unitOfWork, IMovieRepository UserRepository, IMapper mapper, IExceptionManager exceptionManager)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            //this.exceptionManager = exceptionManager;
            this.UserRepository = UserRepository;
            this.exceptionManager = exceptionManager;

        }

        public OperationResult<MovieDTO> Create(MovieDTO item)
        {
            
            MovieDomain movie = mapper.Map<MovieDTO, MovieDomain>(item);

            OperationResult result;
            //- As a normal practice just use repository and UoW to do CUD operations, else see #4 below.
            //2.1. Use repository to add domain entity in DBSet
            UserRepository.Create(movie);

            //3. Save changes to database
            result = unitOfWork.Commit();

            //- Transaction mechanism should be used if there are calls to other AppServices as well.
            //2.2. Begin transaction
            //using (var transaction = UnitOfWork.BeginTransaction())
            //{
            //    //Lets say we have to call another Appservice method here (which may have its own UoW commit).
            //    //this.Delete(item);

            //    //4.1. Use repository to add domain entity in DBSet
            //    _prodRepository.Insert(product);

            //    //4.2. Save changes to database
            //    result = UnitOfWork.Commit();

            //    //4.3. Commit transaction
            //    transaction.CommitTransaction();
            //}

            //5. Map the "Identity" field directly
            item.Id = movie.Id;

            //6. Prepare the response
            return new OperationResult<MovieDTO>(item, result.IsSuccess, result.MainMessage, result.AssociatedMessages.ToList<Message>());
        }

        /// <summary>
        /// Get all Products
        /// </summary>
        /// <returns></returns>
        public OperationResult<IEnumerable<MovieDTO>> GetAllMovies()
        {
            IEnumerable<MovieDomain> movieList = UserRepository.All().ToList<MovieDomain>();

            List<MovieDTO> MovieDTOList = new List<MovieDTO>();
            MovieDTOList = mapper.Map<IEnumerable<MovieDomain>, List<MovieDTO>>(movieList);
            Message message = new Message(string.Empty, "Return Successfully");
            return new OperationResult<IEnumerable<MovieDTO>>(MovieDTOList, true, message);
        }

        public OperationResult<IEnumerable<MovieDTO>> GetUpcomingMovies()
        {
            var upcomingMovies =  UserRepository.Filter(x => x.Category == "Upcoming").ToList();
            
            List<MovieDTO> MovieDTOList = new List<MovieDTO>();
            MovieDTOList = mapper.Map<IEnumerable<MovieDomain>, List<MovieDTO>>(upcomingMovies);
            Message message = new Message(string.Empty, "Return Successfully");
            return new OperationResult<IEnumerable<MovieDTO>>(MovieDTOList, true, message);
        }

        public OperationResult<IEnumerable<MovieDTO>> GetNewReleaseMovies()
        {
            IEnumerable<MovieDomain> newReleaseMovies = UserRepository.Filter(x => x.Category == "New Release").ToList();
            List<MovieDTO> MovieDTOList = new List<MovieDTO>();
            MovieDTOList = mapper.Map<IEnumerable<MovieDomain>, List<MovieDTO>>(newReleaseMovies);
            Message message = new Message(string.Empty, "Return Successfully");
            return new OperationResult<IEnumerable<MovieDTO>>(MovieDTOList, true, message);
        }

        public OperationResult<IEnumerable<MovieDTO>> GetFeaturedMovies()
        {
            IEnumerable<MovieDomain> featuredMovies = UserRepository.Filter(x => x.Category == "Featured").ToList();
            List<MovieDTO> MovieDTOList = new List<MovieDTO>();
            MovieDTOList = mapper.Map<IEnumerable<MovieDomain>, List<MovieDTO>>(featuredMovies);
            Message message = new Message(string.Empty, "Return Successfully");
            return new OperationResult<IEnumerable<MovieDTO>>(MovieDTOList, true, message);
        }

        public bool Find(MovieDTO item)
        {
            Expression<Func<MovieDomain, bool>> expression = u => u.Id == item.Id;
            return UserRepository.Contains(expression);
        }

        public OperationResult<MovieUserDTO> AddMovieToList(MovieUserDTO movieUser)
        {
            MovieUser movie = mapper.Map<MovieUserDTO, MovieUser>(movieUser);
            Expression<Func<MovieUser, bool>> predicate = x => x.MovieId == movieUser.MovieId && x.UserId == movieUser.UserId;
            if (UserRepository.FindMovie(predicate) != null)
            {
                Message message = new Message(String.Empty, "Movie already exist in the list");
                return new OperationResult<MovieUserDTO>(movieUser, true, message);
            }
            UserRepository.AddMovie(movie);
            var result = unitOfWork.Commit();
            
            return new OperationResult<MovieUserDTO>(movieUser, result.IsSuccess, result.MainMessage, result.AssociatedMessages.ToList<Message>());
        }

        public OperationResult<MovieDTO> GetMovieByID(int id)
        {
            Expression<Func<MovieDomain, bool>> expression = m => m.Id == id;
            MovieDomain user = UserRepository.Find(expression);
            if (user != null)
            {
                MovieDTO item = mapper.Map<MovieDomain, MovieDTO>(user);
                Message message = new Message(String.Empty, "Successfully Found");
                return new OperationResult<MovieDTO>(item, true, message);

            }
            else
            {
                MovieDTO item = new MovieDTO();
                Message message = new Message(String.Empty, "User not found");
                return new OperationResult<MovieDTO>(item, false, message);
            }
        }

        public OperationResult<MovieDTO> DeleteMovie(int id)
        {
            UserRepository.Delete(id);
            unitOfWork.Commit();
            var result = this.GetMovieByID(id);
            return result;
        }

        public async Task<OperationResult<IList<MovieDomain>>> GetMovieList(int Id)
        {
            var user = await UserRepository.GetMovieUser(x => x.Id == Id);
            var movies = user.Movies;
            IEnumerable<MovieUserDTO> movie = mapper.Map<ICollection<MovieUser>, ICollection<MovieUserDTO>>(movies).ToList();
            IList<MovieDomain> movieDomain = new List<MovieDomain>();
            foreach(var m in movie)
            {
                movieDomain.Add(m.Movie);
            }
            Message message = new Message(String.Empty, "return successfully");
            return new OperationResult<IList<MovieDomain>>(movieDomain, true, message);
        }
      
    }
}
