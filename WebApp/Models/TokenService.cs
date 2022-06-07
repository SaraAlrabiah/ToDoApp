
using WebApp.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class TokenService : ITokenService
    {

        private readonly IConfiguration _configration;


        public TokenService(IConfiguration configuration)
        {
            _configration = configuration;
        }


        public string BuildToken( UserInfo user)

        {
            var key = Encoding.UTF8.GetBytes(_configration["Jwt:Key"]);
            var issuer = _configration["Jwt:Issuer"];

            var claims = new[] {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString())
            };

            var securityKey = new SymmetricSecurityKey(key);
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new JwtSecurityToken(issuer, issuer, claims,
           expires: DateTime.Now.AddMinutes(double.Parse(_configration["Jwt:Expiration"])), signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }
       
        public bool IsTokenValid( string token)
        {
            var mySecret = Encoding.UTF8.GetBytes(_configration["Jwt:Key"]);
            var issuer = _configration["Jwt:Issuer"];
            ;
            var mySecurityKey = new SymmetricSecurityKey(mySecret);

            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = issuer,
                    ValidAudience = issuer,
                    IssuerSigningKey = mySecurityKey,
                }, out SecurityToken validatedToken);
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}

