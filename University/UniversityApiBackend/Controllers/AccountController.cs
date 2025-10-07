using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.ConstrainedExecution;
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
        [HttpPost]
        public IActionResult GetToken(UserLogins userLogins)
        {
            try
            {
                var Token = new UserTokens();
                // TODO: Hash password
                var user = _context.Users?
                    .Where(u => u.Username.Equals(userLogins.UserName, StringComparison.OrdinalIgnoreCase) && u.Password == userLogins.Password)
                    .FirstOrDefault();

                if (user != null)
                {
                    Console.WriteLine("User found", user.Name);
                    Token = JwtHelpers.GenTokenKey(new UserTokens()
                    {
                        UserName = user.Username,
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
            var users = _context.Users?.ToList();
            return Ok(users);
        }
    }
}
