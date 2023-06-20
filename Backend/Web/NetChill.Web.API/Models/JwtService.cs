using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NetChill.Project.Bussiness.Entities.MovieDomains.AppServices.DTOs;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NetChill.Web.API.Models
{
    public class JwtService
    {
        public String SecretKey { get; set; }
        public int TokenDuration { get; set; }
        private readonly IConfiguration config;

        public JwtService(IConfiguration config)
        {
            this.config = config;
            SecretKey = this.config.GetSection("jwtConfig").GetSection("Key").Value;
            TokenDuration = Int32.Parse(this.config.GetSection("jwtConfig").GetSection("Duration").Value);
        }

        public String GenerateToken(String Id, String Name, String Email, String Role)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.SecretKey));
            var signature = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);// Signature is made up of Key & SigningAlgorithm
            var payload = new[]
            {
                new Claim("id", Id),
                new Claim("name", Name),
                new Claim("email", Email),
                new Claim("role", Role)
            };
            var jwtToken = new JwtSecurityToken( // JwtToken is made up of Signature, Payload
                    issuer: "localhost",
                    audience: "localhost",
                    claims: payload,
                    expires: DateTime.Now.AddMinutes(this.TokenDuration),
                    signingCredentials: signature
                );
            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }
    }
}
