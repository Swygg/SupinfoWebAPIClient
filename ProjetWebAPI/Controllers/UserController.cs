using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ProjetWebAPI.Models.DTO;
using ProjetWebAPI.Models.Inputs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProjetWebAPI.Controllers
{
    [ApiController]
    [Route("/Users")]
    [Authorize(Roles ="type1, type2, type3")]
    public class UserController : Controller
    {
        private readonly IConfiguration _config;
        private List<User> _users = new List<User>()
        {
            new User(){ Username ="Vincent", Password ="123", Role ="Admin"},
            new User(){ Username ="Julie", Password ="123", Role ="Student"},
            new User(){ Username ="Antoine", Password ="123", Role ="Student"},
        };

        public UserController(IConfiguration config)
        {
            _config = config;
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(UserLoginInput userLogin)
        {
            var user = Authenticate(userLogin);
            if (user != null)
            {
                var token = GenerateToken(user);
                return Ok(token);
            }

            return NotFound("user not found");
        }


        private User? Authenticate(UserLoginInput userLogin)
        {
            return _users.FirstOrDefault(x =>
                x.Username.ToLower() == userLogin.Username.ToLower() && 
                x.Password == userLogin.Password);
        }

        // To generate token
        private string GenerateToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,user.Username),
                new Claim(ClaimTypes.Role,user.Role)
            };
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(60*24*7),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
