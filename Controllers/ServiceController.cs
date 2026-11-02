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
    public class ServiceController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ServiceController(AppDbContext context)
        {
            _context = context;
        }

        // ================================
        // ✅ CREATE SERVICE
        // ================================
        [HttpPost]
        public async Task<IActionResult> CreateService(CreateServiceDto dto)
        {
            var service = new Service
            {
                name = dto.name,
                description = dto.description,
                icon = dto.icon,
                isActive = dto.isActive
            };

            _context.services.Add(service);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Service created successfully", serviceId = service.serviceId });
        }

        // ================================
        // ✅ GET ALL SERVICES
        // ================================
        [HttpGet]
        public async Task<IActionResult> GetAllServices()
        {
            var services = await _context.services
                .Select(s => new ServiceResponseDto
                {
                    serviceId = s.serviceId,
                    name = s.name,
                    description = s.description,
                    //icon = s.icon,
                    isActive = s.isActive
                })
                .ToListAsync();

            return Ok(services);
        }

        // ================================
        // ✅ GET SERVICE BY ID
        // ================================
        [HttpGet("{id}")]
        public async Task<IActionResult> GetServiceById(int id)
        {
            var service = await _context.services
                .Where(s => s.serviceId == id)
                .Select(s => new ServiceResponseDto
                {
                    serviceId = s.serviceId,
                    name = s.name,
                    description = s.description,
                   // icon = s.icon,
                    isActive = s.isActive
                })
                .FirstOrDefaultAsync();

            if (service == null) return NotFound();
            return Ok(service);
        }

        // ================================
        // ✅ UPDATE SERVICE
        // ================================
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateService(int id, [FromBody] CreateServiceDto dto)
        {
            var service = await _context.services.FindAsync(id);
            if (service == null) return NotFound();

            service.name = dto.name;
            service.description = dto.description;
            service.icon = dto.icon;
            service.isActive = dto.isActive;

            await _context.SaveChangesAsync();
            return Ok(new { message = "Service updated successfully" });
        }

        // ================================
        // ✅ DELETE SERVICE
        // ================================
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteService(int id)
        {
            var service = await _context.services.FindAsync(id);
            if (service == null) return NotFound();

            _context.services.Remove(service);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Service deleted successfully" });
        }
    
}
}
