using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using workerFinal.Data;
using workerFinal.Models;
using workerFinal.DTO;
namespace workerFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoritesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public FavoritesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/favorites?userId=1
        [HttpGet]
        public async Task<IActionResult> GetUserFavorites(int userId)
        {
            var favorites = await _context.favorites
                .Where(f => f.userId == userId)
                .Select(f => f.taskerId)
                .ToListAsync();

            return Ok(favorites);
        }

        // POST: api/favorites/toggle
        [HttpPost("toggle")]
        public async Task<IActionResult> ToggleFavorite([FromBody] ToggleFavoriteDto dto)
        {
            var existing = await _context.favorites
                .FirstOrDefaultAsync(f =>
                    f.userId == dto.userId &&
                    f.taskerId == dto.taskerId);

            if (existing == null)
            {
                _context.favorites.Add(new Favorite
                {
                    userId = dto.userId,
                    taskerId = dto.taskerId
                });
            }
            else
            {
                _context.favorites.Remove(existing);
            }

            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
