using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using workerFinal.Data;
using workerFinal.DTO;
using workerFinal.Models;
namespace workerFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly AppDbContext _context;

        public UserController(AppDbContext context)
        {
            _context = context;
        }

        // ================================
        // ✅ CREATE USER
        // ================================
        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserDto dto)
        {
            var user = new User
            {
                fullName = dto.fullName,
                email = dto.email,
                passwordHash = dto.passwordHash,
                phone = dto.phone,
                role = dto.role,
                createdAt = DateTime.UtcNow,
                isActive = dto.isActive
            };

            _context.users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(new { message = "User created successfully", userId = user.userId });
        }

        // ================================
        // ✅ GET ALL USERS
        // ================================
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _context.users
                .Select(u => new UserResponseDto
                {
                    userId = u.userId,
                    fullName = u.fullName,
                    email = u.email,
                    phone = u.phone,
                    role = u.role.ToString(),
                    isActive = u.isActive,
                    createdAt = u.createdAt,
                    profileImage = u.profileImage
                })
                .ToListAsync();

            return Ok(users);
        }

        // ================================
        // ✅ GET USER BY ID
        // ================================
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _context.users
                .Where(u => u.userId == id)
                .Select(u => new UserResponseDto
                {
                    userId = u.userId,
                    fullName = u.fullName,
                    email = u.email,
                    phone = u.phone,
                    role = u.role.ToString(),
                    isActive = u.isActive,
                    createdAt = u.createdAt,
                    profileImage = u.profileImage
                })
                .FirstOrDefaultAsync();

            if (user == null) return NotFound();

            return Ok(new{
            userId=user.userId,
            fullName=user.fullName,
            email=user.email,
            phone=user.phone,
            role=user.role.ToString(),
            profileImage=user.profileImage
            }
                
                
                
                );
        }
        [HttpPost("upload-profile-image/{userId}")]
        public async Task<IActionResult> UploadProfileImage(int userId, IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");

            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);
            var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);

            //var fileName = $"user_{userId}{Path.GetExtension(file.FileName)}";

            var filePath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var user = await _context.users.FindAsync(userId);

            if (user == null)
                return NotFound();

            user.profileImage = "/uploads/" + fileName;

            await _context.SaveChangesAsync();

            return Ok(new { imagePath = user.profileImage });
        }


        // ================================
        // ✅ UPDATE USER
        // ================================
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] CreateUserDto dto)
        {
            var user = await _context.users.FindAsync(id);
            if (user == null) return NotFound();

            user.fullName = dto.fullName;
            user.email = dto.email;
           // user.passwordHash = dto.passwordHash;
            user.phone = dto.phone;
           // user.city = dto.city;
           // user.role = dto.role;
            user.isActive = dto.isActive;
           
            await _context.SaveChangesAsync();
            return Ok(new { message = "User updated successfully" });
        }

        // ================================
        // ✅ DELETE USER
        // ================================
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.users.FindAsync(id);
            if (user == null) return NotFound();

            _context.users.Remove(user);
            await _context.SaveChangesAsync();

            return Ok(new { message = "User deleted successfully" });
        }
    
}
}
