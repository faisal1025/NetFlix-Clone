using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NetChill.Project.Foundation.Core.ValueObjects;
using NetChill.Project.UserDomains.AppServices;
using NetChill.Project.UserDomains.AppServices.DTOs;
using NetChill.Web.API.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetChill.Web.API.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserAppService userAppService;
        private readonly IConfiguration config;
        public UserController(IUserAppService userAppService, IConfiguration config)
        {
            this.userAppService = userAppService;
            this.config = config;
        }

        // api/User/register
        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register(UserDTO user)
        {
            
            var result = userAppService.Create(user);
           
            var Json = JsonConvert.SerializeObject(result);
            return Ok(Json);
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public IActionResult Login(UserDTO user)
        {
            var result = userAppService.Login(user);
            if (result.IsSuccess)
            {
                var _user = result.Data;
                var token = new JwtService(this.config).GenerateToken(_user.Id.ToString(), _user.UserName, _user.UserEmail, _user.Role);

                Message message = new Message(code: "true", text: token);
                var Json = JsonConvert.SerializeObject(message);
                return Ok(Json);
            }
            else
            {
                var info = result.MainMessage.Text;
                Message message = new Message(code: "false", text:info);
                var Json = JsonConvert.SerializeObject(message);
                return Ok(Json);
            }
        }

     
        
        [HttpGet("getUsers")]
        public IActionResult GetUsers()
        {
            var result = userAppService.GetAllUsers();
            var Json = JsonConvert.SerializeObject(result);
            return Ok(Json);
        }

        [HttpDelete("delete/{id}")]
        public IActionResult DeleteUser(int id)
        {
            var result = userAppService.DeleteUser(id);
            Console.WriteLine(result.IsSuccess);
            if (!result.IsSuccess)
            {
                // true user deleted
                Message message = new Message(code: "true", text: "User successfully deleted");
                var Json = JsonConvert.SerializeObject(message);
                return Ok(Json);

            }
            else
            {
                Message message = new Message(code: "false", text: "User not deleted something went wrong!");
                var Json = JsonConvert.SerializeObject(message);
                return Ok(Json);
            }
        }
        
    }
}
