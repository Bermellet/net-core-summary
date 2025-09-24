using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UniversityApiBackend.DataAccess;
using UniversityApiBackend.Helpers;
using UniversityApiBackend.Models;
using UniversityApiBackend.Models.DataModels;

namespace UniversityApiBackend.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly JwtSettings _jwtSettings;
        private readonly UniversityDBContext _context;


        public AccountController(JwtSettings jwtSettings, UniversityDBContext context)
        {
            _jwtSettings = jwtSettings;
            _context = context;
        }

        // TODO: Change by real users in BD (using _context)
        private IEnumerable<User> Logins = new List<User>()
        {
            new User()
            {
                Id = 1,
                Email = "martin@imaginagroup.com",
                Name = "Admin",
                Password = "Admin"
            },
            new User()
            {
                Id = 2,
                Email = "pepe@imaginagroup.com",
                Name = "User1",
                Password = "pepe"
            },
        };

        [HttpPost]
        public IActionResult GetToken(UserLogins userLogins)
        {
            try
            {
                var Token = new UserTokens();
                var user = Logins.FirstOrDefault(user => user.Name.Equals(userLogins.UserName, StringComparison.OrdinalIgnoreCase));

                if (user != null)
                {
                    Token = JwtHelpers.GenTokenKey(new UserTokens()
                    {
                        UserName = user.Name,
                        EmailId = user.Email,
                        Id = user.Id,
                        GuidId = Guid.NewGuid()
                    }, _jwtSettings);
                }
                else
                {
                    return BadRequest("Wrong Username/Password");
                }
                return Ok(Token);
            }
            catch (Exception ex)
            {
                throw new Exception("GetToken error", ex);
            }
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public IActionResult GetUserList()
        {
            return Ok(Logins);
        }
    }
}
