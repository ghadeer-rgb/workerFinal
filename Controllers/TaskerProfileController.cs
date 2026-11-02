using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using workerFinal.Data;
using workerFinal.DTO;
using workerFinal.Models;

using Microsoft.AspNetCore.Mvc;

namespace workerFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskerProfileController : ControllerBase
    {

        private readonly AppDbContext _context;

        public TaskerProfileController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPut("set-service/{taskerId}")]
        public async Task<IActionResult> SetService(int taskerId, [FromBody] int serviceId)
        {
            var profile = await _context.taskerProfiles
                .FirstOrDefaultAsync(tp => tp.taskerId == taskerId);

            if (profile == null)
            {
                profile = new TaskerProfile
                {
                    taskerId = taskerId,
                    serviceId = serviceId,
                    bio = "",
                    experienceYears = 0,
                    hourlyRate = 0,
                    city = "",
                    isApproved = false

                };
                _context.taskerProfiles.Add(profile);
            }
            else
            {
                profile.serviceId = serviceId;
            }
                await _context.SaveChangesAsync();
            

            return Ok(new { message = "Service updated successfully" });
        }


        // ================================
        // ✅ CREATE OR UPDATE TASKER PROFILE
        // ================================
        [HttpPost]
        public async Task<IActionResult> CreateOrUpdateTaskerProfile(CreateTaskerProfileDto dto)
        {
            var profile = await _context.taskerProfiles
                .FirstOrDefaultAsync(tp => tp.taskerId == dto.taskerId);

            if (profile == null)
            {
                profile = new TaskerProfile
                {
                    taskerId = dto.taskerId,
                    serviceId = dto.serviceId,
                    bio = dto.bio,
                    experienceYears = dto.experienceYears,
                    hourlyRate = dto.hourlyRate,
                    city = dto.city,
                    isApproved = dto.isApproved
                };
                _context.taskerProfiles.Add(profile);
            }
            else
            {
                profile.serviceId = dto.serviceId;
                profile.bio = dto.bio;
                profile.experienceYears = dto.experienceYears;
                profile.hourlyRate = dto.hourlyRate;
                profile.city = dto.city;
                profile.isApproved = dto.isApproved;
            }

            await _context.SaveChangesAsync();
            return Ok(new { message = "Tasker profile saved successfully" });
        }

        // ================================
        // ✅ GET ALL TASKER PROFILES
        // ================================
        [HttpGet]
        public async Task<IActionResult> GetAllTaskerProfiles()
        {
            var profiles = await _context.taskerProfiles
                .Include(tp => tp.tasker)
                .Include(tp => tp.service)
                .Select(tp => new TaskerProfileResponseDto
                {
                    taskerId = tp.taskerId,
                    taskerName = tp.tasker.fullName,
                    serviceId = tp.serviceId,
                    serviceName = tp.service.name,
                    bio = tp.bio,
                    experienceYears = tp.experienceYears,
                    hourlyRate = tp.hourlyRate,
                    city = tp.city,
                    ratingAverage = tp.ratingAverage,
                    isApproved = tp.isApproved,
                    profileImage=tp.tasker.profileImage
                })
                .ToListAsync();

            return Ok(profiles);
        }

        // ================================
        // ✅ GET PROFILE BY TASKER ID
        // ================================
        [HttpGet("{taskerId}")]
        public async Task<IActionResult> GetProfileByTaskerId(int taskerId)
        {
            var profile = await _context.taskerProfiles
                .Include(tp => tp.tasker)
                .Include(tp => tp.service)
                .Where(tp => tp.taskerId == taskerId)
                .Select(tp => new TaskerProfileResponseDto
                {
                    taskerId = tp.taskerId,
                    taskerName = tp.tasker.fullName,
                    serviceId = tp.serviceId,
                    serviceName = tp.service.name,
                    bio = tp.bio,
                    experienceYears = tp.experienceYears,
                    hourlyRate = tp.hourlyRate,
                    city = tp.city,
                    ratingAverage = tp.ratingAverage,
                    isApproved = tp.isApproved,
                  // profileImage = tp.tasker.profileImage
                })
                .FirstOrDefaultAsync();

            if (profile == null) return NotFound();
            return Ok(profile);


            //return Ok(new
            //{
            //    userId = user.userId,
            //    fullName = user.fullName,
            //    email = user.email,
            //    phone = user.phone,
            //    role = user.role.ToString(),
            //    profileImage = user.profileImage
            //}
        }

        // ================================
        // ✅ DELETE TASKER PROFILE
        // ================================
        [HttpDelete("{taskerId}")]
        public async Task<IActionResult> DeleteProfile(int taskerId)
        {
            var profile = await _context.taskerProfiles.FindAsync(taskerId);
            if (profile == null) return NotFound();

            _context.taskerProfiles.Remove(profile);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Tasker profile deleted successfully" });
        }
        [HttpGet("public/{taskerId}")]
        public async Task<IActionResult> GetPublicProfile(int taskerId)
        {
            var profile = await _context.taskerProfiles
                .Include(tp => tp.tasker)
                .Include(tp => tp.service)
                .Where(tp => tp.taskerId == taskerId && tp.isApproved)
                .Select(tp => new TaskerPublicProfileDto
                {
                    taskerId = tp.taskerId,
                    taskerName = tp.tasker.fullName,
                    serviceName = tp.service.name,
                    bio = tp.bio,
                    experienceYears = tp.experienceYears,
                    hourlyRate = tp.hourlyRate,
                    city = tp.city,
                    ratingAverage = tp.ratingAverage
                })
                .FirstOrDefaultAsync();

            if (profile == null)
                return NotFound();

            return Ok(profile);
        }

        //[HttpPost("{taskerId}/upload")]
        //public async Task<IActionResult> UploadImage(int taskerId, IFormFile file)
        //{
        //    var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
        //    var path = Path.Combine("wwwroot/images", fileName);

        //    using var stream = new FileStream(path, FileMode.Create);
        //    await file.CopyToAsync(stream);

        //    var profile = await _context.taskerProfiles.FindAsync(taskerId);
        //    profile.profileImageUrl = "/images/" + fileName;

        //    await _context.SaveChangesAsync();

        //    return Ok();
        //}


        // GET: api/TaskerProfile/byService/1
        [HttpGet("byService/{serviceId}")]
        public async Task<IActionResult> GetTaskersByService(int serviceId)
        {
            var taskers = await _context.taskerProfiles
                .Include(tp => tp.tasker)
                .Include(tp => tp.service)
                .Where(tp => tp.serviceId == serviceId && tp.isApproved)
                .Select(tp => new TaskerProfileResponseDto
                {
                    taskerId = tp.taskerId,
                    taskerName = tp.tasker.fullName,
                    serviceId = tp.serviceId,
                    serviceName = tp.service.name,
                    bio = tp.bio,
                    experienceYears = tp.experienceYears,
                    hourlyRate = tp.hourlyRate,
                    city = tp.city,
                    ratingAverage = tp.ratingAverage,
                    isApproved = tp.isApproved,
                    profileImage = tp.tasker.profileImage
                })
                .ToListAsync();

            return Ok(taskers);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchTaskers(
    int? serviceId,
    string? city,
    string? sort)
        {
            var query = _context.taskerProfiles
                .Include(tp => tp.tasker)
                .Include(tp => tp.service)
                .Where(tp => tp.isApproved)
                .AsQueryable();

            // FILTER BY SERVICE
            if (serviceId.HasValue)
                query = query.Where(tp => tp.serviceId == serviceId);

            // FILTER BY CITY
            if (!string.IsNullOrEmpty(city))
                query = query.Where(tp => tp.city.Contains(city));

            // SORTING
            query = sort switch
            {
                "rating" => query.OrderByDescending(tp => tp.ratingAverage),
                "price" => query.OrderBy(tp => tp.hourlyRate),
                _ => query
            };

            var result = await query
                .Select(tp => new TaskerProfileResponseDto
                {
                    taskerId = tp.taskerId,
                    taskerName = tp.tasker.fullName,
                    serviceId = tp.serviceId,
                    serviceName = tp.service.name,
                    bio = tp.bio,
                    experienceYears = tp.experienceYears,
                    hourlyRate = tp.hourlyRate,
                    city = tp.city,
                    ratingAverage = tp.ratingAverage,
                    isApproved = tp.isApproved,
                   profileImage = tp.tasker.profileImage
                })
                .ToListAsync();

            return Ok(result);
        }
    }
}

