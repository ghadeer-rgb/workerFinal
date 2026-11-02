using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using workerFinal.Data;
using workerFinal.DTO;
using workerFinal.Models;
namespace workerFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly AppDbContext _context;

        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var user = await _context.users
                .FirstOrDefaultAsync(u =>
                    u.email == dto.email &&
                    u.passwordHash == dto.password); // ⚠️ simple check (no hashing yet)

            if (user == null)
                return Unauthorized("Invalid email or password");

            return Ok(new
            {
                token = "fake-token", // later JWT
                role = user.role,
                id = user.userId,
                fullName = user.fullName,
                profileImage = user.profileImage
            });
        }

    }
}
