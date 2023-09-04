using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MailKit.Net.Smtp;
using NetChill.Project.Bussiness.Entities.Services.EmailServices;
using NetChill.Project.Bussiness.Entities.UserDomains.AppServices.DTOs;
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
        private readonly IEmailService emailService;
        public UserController(IUserAppService userAppService, IConfiguration config, IEmailService emailService)
        {
            this.userAppService = userAppService;
            this.config = config;
            this.emailService = emailService;
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


        [Authorize(Roles = "Admin")]
        [HttpGet("getUsers")]
        public IActionResult GetUsers()
        {
            var result = userAppService.GetAllUsers();
            var Json = JsonConvert.SerializeObject(result);
            return Ok(Json);
        }

        [Authorize(Roles = "Admin")]
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

        [HttpPost("sendRecoveryEmail")]
        public async Task<IActionResult> SendRecoveryEmail(UserDTO userDTO)
        {
            try
            {
                var user = userAppService.GetUserByEmail(userDTO.UserEmail);
                if(user != null)
                {
                    var token = new JwtService(config).GenerateToken(user.Id.ToString(), user.UserName, user.UserEmail, user.Role);
                    var appDomain = config.GetSection("Application").GetSection("AppDomain").Value;
                    var resetLink = config.GetSection("Application").GetSection("ResetLink").Value;
                    SendEmailOptions sendEmailOptions = new SendEmailOptions()
                    {
                        SendTo = new List<string>() { userDTO.UserEmail },
                        Placeholder = new List<KeyValuePair<string, string>>() { 
                            new KeyValuePair<string, string>("{{UserName}}", user.UserName),
                            new KeyValuePair<string, string>("{{Link}}", string.Format(appDomain+resetLink, user.Id, token.ToString()))  
                        },
                    };
                    await emailService.SendRecoveryEmail(sendEmailOptions);
                    Message message = new Message(code: "true", text: "A reset email is sent to your email if email is registered");
                    var Json = JsonConvert.SerializeObject(message);
                    return Ok(Json);
                }
                else
                {
                    Message message = new Message(code: "false", text: "Email is not registered");
                    var Json = JsonConvert.SerializeObject(message);
                    return StatusCode(StatusCodes.Status204NoContent, Json);
                }
            }
            catch (Exception)
            {

                Message message = new Message(code: "false", text: "Something Went Wrong, Try Again");
                var Json = JsonConvert.SerializeObject(message);
                return Ok(Json);
            }
        }
        
    }
}
