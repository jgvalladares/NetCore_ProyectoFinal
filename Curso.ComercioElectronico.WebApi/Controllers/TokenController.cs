
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Text;

namespace Curso.ComercioElectronico.WebApi.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase 
    {

        private readonly JwtConfiguration jwtConfiguration;

        public TokenController(IOptions<JwtConfiguration> options) {
            this.jwtConfiguration = options.Value;
        }


        [HttpPost]
        public async Task<string> TokenAsync(UserInput input)
        {

            //1. Validar User.
            /*var userTest = "foo";
            if (input.UserName != userTest || input.Password != "123")
            {
                throw new AuthenticationException("User or Passowrd incorrect!");
            }*/

            //Roles de Usuarios 
            var users = new[] {
                new {User= "Gaby", Roles = new [] { "Admin"}},
                new {User= "Support", Roles = new [] { "Support"}},
                new {User= "user", Roles = new [] { "Accountant,Finance, Manager"}},
            };

            //1. Validar Users
            if (!users.Any(x => x.User.Equals(input.UserName)) || input.Password != "admin")
            {
                throw new AuthenticationException("User or Passowrd incorrect!");
            }

            //2. Generar claims
            //create claims details based on the user information
            var claims = new List<Claim>(); //Lista de claims 

            var user = users.Single(x => x.User.Equals(input.UserName));

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.User));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()));
            claims.Add(new Claim("UserName", input.UserName));

            foreach (var item in user.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, item));
                claims.Add(new Claim("Cliente", true.ToString()));
            }

            
            claims.Add(new Claim("Foo","Bar"));
            /*
            var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, userTest),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("UserName", userTest),
                        //new Claim("Email", user.Email)
                        //Other...
                        //Option 1 
                        new Claim("Roles", "Admin,Support"),

                        //Option 2
                        new Claim("Roles", "Support"),
                        new Claim("Roles", "Admin")
                    };
            */

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfiguration.Key));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var tokenDescriptor = new JwtSecurityToken(
                jwtConfiguration.Issuer,
                jwtConfiguration.Audience,
                claims,
                expires: DateTime.UtcNow.Add(jwtConfiguration.Expires),
                signingCredentials: signIn);
 

           var jwt = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);


            return jwt;
        }   
    }

    public class UserInput
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
    }
}
