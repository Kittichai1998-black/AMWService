using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using AMWService.IdentityAuth;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using AMWService.Models;
using AMWService.DbContext;
using Microsoft.EntityFrameworkCore;

namespace AMWService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private readonly DbConfig _context;

        public AuthenticateController(UserManager<User>userManager, IConfiguration configuration,DbConfig context)
        {
            _userManager = userManager;
            _configuration = configuration;
            _context = context;
        }

        //[HttpGet("GetUser")]
        //public async Task<ActionResult<IEnumerable<UserOwner>>> Getamv_User()
        //{
        //    return await _context.amv_User.ToListAsync();
        //}

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] Register model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Username);
            if (userExists != null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User Already Exists!" });
            }

            User user = new User()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username,
            };

            var result = await _userManager.CreateAsync(user, model.Password);  
            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User Creation Failed!" });
            }
            return Ok(new Response { Status = "Success", Message = "User Create Successfully!" });
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(Login model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var authCleaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name ,user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
                };
                foreach (var userRole in userRoles)
                {
                    authCleaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));

                var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddHours(3),
                    claims: authCleaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );

                return Ok(new {User = user.UserName,Email = user.Email,accessToken = new JwtSecurityTokenHandler().WriteToken(token), expiration = token.ValidTo, status = "Success"});

            }
            return Unauthorized();
        }
    }
}
