using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using NetChill.Project.Bussiness.Entities.MovieDomains.AppServices.DTOs;
using NetChill.Project.Foundation.Core.ValueObjects;
using NetChill.Project.MovieDomains.AppServices;
using NetChill.Project.MovieDomains.AppServices.DTOs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace NetChill.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieAppService movieAppServive;
        private readonly IWebHostEnvironment webHostEnvironment;
        public MoviesController(IMovieAppService movieAppService, IWebHostEnvironment webHostEnvironment)
        {
            this.movieAppServive = movieAppService;
            this.webHostEnvironment = webHostEnvironment;
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
        }

       
        [HttpPost("send")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> saveMovie([FromForm] MovieDTO movie)
        {
            try
            {
                var url = Environment.GetEnvironmentVariable("ASPNETCORE_URL");
                string posterfolder = "movies/poster/";
                posterfolder += Guid.NewGuid().ToString() + "_" + movie.MoviePoster.FileName;
                movie.ImageName = url + posterfolder;
                string serverPath = Path.Combine(webHostEnvironment.WebRootPath, posterfolder);

                await movie.MoviePoster.CopyToAsync(new FileStream(serverPath, FileMode.Create));

                string videofolder = "movies/video/";
                videofolder += Guid.NewGuid().ToString() + "_" + movie.ContentPath.FileName;       
                movie.VideoName = url + videofolder;
          
                serverPath = Path.Combine(webHostEnvironment.WebRootPath, videofolder);

                await movie.ContentPath.CopyToAsync(new FileStream(serverPath, FileMode.Create));
                var result = movieAppServive.Create(movie);
                var Json = JsonConvert.SerializeObject(result);
                return Ok(Json);
            }
            catch (Exception)
            {
                throw;
            }

        }

        [HttpGet("all")]
        public IActionResult getMovie()
        {
            var AllMovies = movieAppServive.GetAllMovies();
            var UpcomingMovies = movieAppServive.GetUpcomingMovies();
            var NewReleaseMovies = movieAppServive.GetNewReleaseMovies();
            var FeaturedMovies = movieAppServive.GetFeaturedMovies();
            Message message = new Message(String.Empty, "Successfully load");
            var result = new OperationResult<IEnumerable<MovieDTO>>(AllMovies.Data, UpcomingMovies.Data, NewReleaseMovies.Data, FeaturedMovies.Data, true, message);
            var Json = JsonConvert.SerializeObject(result);
            return Ok(Json);
        }

        [HttpGet("allMovies")]
        public IActionResult getAllMovies()
        {
            var AllMovies = movieAppServive.GetAllMovies();
            Message message = new Message(String.Empty, "Successfully load");
            var result = new OperationResult<IEnumerable<MovieDTO>>(AllMovies.Data, true, message);
            var Json = JsonConvert.SerializeObject(result);
            return Ok(Json);
        }

        [HttpGet("upcoming")]
        public IActionResult getUpcomingMovies()
        {
            var result = movieAppServive.GetUpcomingMovies();
            var Json = JsonConvert.SerializeObject(result);
            return Ok(Json);
        }

        [HttpGet("newRelease")]
        public IActionResult getNewReleaseMovies()
        {
            var result = movieAppServive.GetNewReleaseMovies();
            var Json = JsonConvert.SerializeObject(result);
            return Ok(Json);
        }

        [HttpGet("featured")]
        public IActionResult getFeaturedMovies()
        {
            var result = movieAppServive.GetFeaturedMovies();
            var Json = JsonConvert.SerializeObject(result);
            return Ok(Json);
        }

        [HttpGet("movie/{id}")]
        public IActionResult getMovie(int id)
        {
            var result = movieAppServive.GetMovieByID(id);
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            };
            var Json = JsonConvert.SerializeObject(result);
            return Ok(Json);
        }

        [HttpGet("myList/{id}")]
        public async Task<IActionResult> getList(int id)
        {
            var result = await movieAppServive.GetMovieList(id);
            var Json = JsonConvert.SerializeObject(result);
            return Ok(Json);
        }

        [HttpPost("addList")]
        public IActionResult addList(MovieUserDTO movieUser)
        {
            var result = movieAppServive.AddMovieToList(movieUser);
            var Json = JsonConvert.SerializeObject(result);
            return Ok(Json);
        }
    }
}
