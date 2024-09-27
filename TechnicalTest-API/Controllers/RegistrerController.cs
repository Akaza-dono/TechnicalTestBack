using DataBase.Context;
using DataBase.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using System.Security.Cryptography;
using TechnicalTest_API.DTO_s;

namespace TechnicalTest_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrerController : ControllerBase
    {
        private readonly TechnicalTestContext _Context;

        public RegistrerController(TechnicalTestContext testContext) {

            _Context = testContext;

        }

        [HttpPost]
        public async Task<ActionResult<Users>> Register(UserDto user)
        {
            var User = await _Context.Users.FirstOrDefaultAsync(e => e.Name == user.UserName);
            if (User != null)
            {
                return BadRequest("User already exists.");
            }
            CreatePasswordHash(user.Password, out byte[] passwordHash, out byte[] passwordSalt);
            var newUser = new Users
            {
                Name = user.UserName,
                HashPassword = passwordHash,
                SaltPassword = passwordSalt,
                IdRole = user.idRole
            };
            _Context.Users.Add(newUser);
            await _Context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUserById), new { id = newUser.Id }, newUser);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Users>> GetUserById(int id)
        {
            var user = await _Context.Users.FirstOrDefaultAsync(e => e.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        public static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using(var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

            }
        }

    }
}
