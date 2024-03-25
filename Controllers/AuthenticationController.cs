using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MrBestPizza.AuthenticationFile;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace MrBestPizza.Controllers
{
    [ApiController]
    [Route("api/authentication")]
    public class AuthenticationController : Controller
    {
        private IConfiguration _configuration;

        [HttpPost("authenticate")]
        public ActionResult<string> Authenticate(AuthenticationRequestBody
            authenticationRequestBody)
        {
            // validate user
            var user = ValidateUserCredentials(
               authenticationRequestBody.username,
               authenticationRequestBody.password);
            if (user == null)
            {
                return Unauthorized();
            }
            //create key
            var securityKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration.GetValue<string>("Authentication:SecretForKey")));
            var signingCredentials = new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha256);
            //claims
            var claimsForToken = new List<Claim>();
            claimsForToken.Add(new("sub", user.UserId.ToString()));
            claimsForToken.Add(new("given_name", user.FirstName));
            claimsForToken.Add(new("family_name", user.LastName));
            //create token
            var jwtToken = new JwtSecurityToken(
              _configuration.GetValue<string>("Authentication:Issuer"),
              _configuration.GetValue<string>("Authentication:Audience"),
              claimsForToken,
              DateTime.UtcNow,
              DateTime.UtcNow.AddHours(1)
              );

            var tokenToReturn = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            return Ok(tokenToReturn);
         
        }
        public AuthenticationController( IConfiguration configuration)
        {
           _configuration = configuration 
                ?? throw new ArgumentNullException(nameof(configuration));
        }

        private InfoUser ValidateUserCredentials(string username, string password)
        {
            return new InfoUser(
                1,
                username ?? "",
                "Chibueze",
                "Geoffrey");
           
        }
    }
}
