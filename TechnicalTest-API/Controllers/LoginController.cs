using DataBase.Context;
using DataBase.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using TechnicalTest_API.DTO_s;

namespace TechnicalTest_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        private readonly TechnicalTestContext _Context;
        private readonly IConfiguration _configuration;
        public LoginController(TechnicalTestContext testContext, IConfiguration configuration)
        {
            _Context       = testContext;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<ActionResult<string>> Login(UserDto request)
        {
            var user = await _Context.Users.FirstOrDefaultAsync(e => e.Name == request.UserName);
            if (user != null && VerifyPasswordHash(request.Password, user.SaltPassword, user.HashPassword))
            {
                string? role = await _Context.Rols.Where(e => e.RolId == user.IdRole).Select(e => e.RolDescription).FirstOrDefaultAsync();
                var UserDto = new UserDto()
                {
                    Role = role,
                    UserName = request.UserName
                };
                return Ok(CreateToken(UserDto));
            }
            return BadRequest("User not found or password is wrong.");

        }

        private bool VerifyPasswordHash(string password, byte[] sendedPasswordSalt, byte[] passwordHash)
        {
            using (var hmac = new HMACSHA512(sendedPasswordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        private string CreateToken(UserDto user)
        {
            var  token = _configuration.GetSection("AppSettings:Token").Value;
            List<Claim> claim = new()
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, user.Role),
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(token));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenPayload = new JwtSecurityToken(
                    
                    claims : claim,
                    expires : DateTime.Now.AddMinutes(5),
                    signingCredentials : cred

                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(tokenPayload);

            return jwt;
        }

    }
}
