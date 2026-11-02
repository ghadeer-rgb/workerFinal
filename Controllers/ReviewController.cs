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
    public class ReviewController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ReviewController(AppDbContext context)
        {
            _context = context;
        }


        // ================================
        // ✅ CREATE REVIEW
        // ================================
        [HttpPost]
        public async Task<IActionResult> CreateReview(CreateReviewDto dto)
        {
            var review = new Review
            {
                taskId = dto.taskId,
                customerId = dto.customerId,
                taskerId = dto.taskerId,
                rating = dto.rating,
                comment = dto.comment,
                createdAt = DateTime.UtcNow
            };

            _context.reviews.Add(review);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Review created successfully", reviewId = review.reviewId });
        }

        // ================================
        // ✅ GET ALL REVIEWS
        // ================================
        [HttpGet]
        public async Task<IActionResult> GetAllReviews()
        {
            var reviews = await _context.reviews
                .Include(r => r.customer)
                .Include(r => r.tasker)
                .Select(r => new ReviewResponseDto
                {
                    reviewId = r.reviewId,
                    taskId = r.taskId,
                    customerId = r.customerId,
                    taskerId = r.taskerId,
                    rating = r.rating,
                    comment = r.comment,
                    createdAt = r.createdAt,
                    customerName = r.customer.fullName,
                    taskerName = r.tasker.fullName
                })
                .ToListAsync();

            return Ok(reviews);
        }

        // ================================
        // ✅ GET REVIEW BY ID
        // ================================
        [HttpGet("{id}")]
        public async Task<IActionResult> GetReviewById(int id)
        {
            var review = await _context.reviews
                .Include(r => r.customer)
                .Include(r => r.tasker)
                .Where(r => r.reviewId == id)
                .Select(r => new ReviewResponseDto
                {
                    reviewId = r.reviewId,
                    taskId = r.taskId,
                    customerId = r.customerId,
                    taskerId = r.taskerId,
                    rating = r.rating,
                    comment = r.comment,
                    createdAt = r.createdAt,
                    customerName = r.customer.fullName,
                    taskerName = r.tasker.fullName
                })
                .FirstOrDefaultAsync();

            if (review == null) return NotFound();

            return Ok(review);
        }

        // ================================
        // ✅ UPDATE REVIEW
        // ================================
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReview(int id, [FromBody] CreateReviewDto dto)
        {
            var review = await _context.reviews.FindAsync(id);
            if (review == null) return NotFound();

            review.rating = dto.rating;
            review.comment = dto.comment;
            review.customerId = dto.customerId;
            review.taskerId = dto.taskerId;
            review.taskId = dto.taskId;

            await _context.SaveChangesAsync();
            return Ok(new { message = "Review updated successfully" });
        }

        // ================================
        // ✅ DELETE REVIEW
        // ================================
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            var review = await _context.reviews.FindAsync(id);
            if (review == null) return NotFound();

            _context.reviews.Remove(review);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Review deleted successfully" });
        }
    }
}

