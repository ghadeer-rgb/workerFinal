
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using workerFinal.Data;
using workerFinal.Models;
using workerFinal.DTO;
namespace workerFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class JobController:ControllerBase
    {
        private readonly AppDbContext _context;

        public JobController(AppDbContext context)
        {
            _context = context;
        }


        [HttpPost]
        public async Task<IActionResult> CreateJob(CreateJobDto dto)
        {
            var job = new Job
            {
                customerId = dto.customerId,
                taskerId = dto.taskerId,
                serviceId = dto.serviceId,
                title = dto.title,
                description = dto.description,
                address = dto.address,
                city = dto.city,
                scheduledDate = dto.scheduledDate,
                status = taskStatus.Assigned,
                createdAt = DateTime.UtcNow
            };

            _context.jobs.Add(job);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Job created successfully", jobId = job.taskId });
        }




        // ================================
        // ✅ GET ALL JOBS
        // ================================
        [HttpGet]
        public async Task<IActionResult> GetAllJobs()
        {
            var jobs = await _context.jobs
                .Include(j => j.customer)
                .Include(j => j.tasker)
                .Include(j => j.service)
                .Select(j => new JobResponseDto
                {
                    taskId = j.taskId,
                    title = j.title,
                    description = j.description,
                    status = j.status.ToString(),
                    scheduledDate = j.scheduledDate,
                    customerName = j.customer.fullName,
                    taskerName = j.tasker != null ? j.tasker.fullName : "",
                    serviceName = j.service.name
                })
                .ToListAsync();

            return Ok(jobs);
        }

        // ================================
        // ✅ GET JOB BY ID
        // ================================
        [HttpGet("{id}")]
        public async Task<IActionResult> GetJobById(int id)
        {
            var job = await _context.jobs
                .Include(j => j.customer)
                .Include(j => j.tasker)
                .Include(j => j.service)
                .Where(j => j.taskId == id)
                .Select(j => new JobResponseDto
                {
                    taskId = j.taskId,
                    title = j.title,
                    description = j.description,
                    status = j.status.ToString(),
                    scheduledDate = j.scheduledDate,
                    customerName = j.customer.fullName,
                    taskerName = j.tasker != null ? j.tasker.fullName : "",
                    serviceName = j.service.name
                })
                .FirstOrDefaultAsync();

            if (job == null)
                return NotFound();

            return Ok(job);
        }

        // ================================
        // ✅ GET JOBS BY CUSTOMER
        // ================================
        //[HttpGet("customer/{customerId}")]
        //public async Task<IActionResult> GetJobsByCustomer(int customerId)
        //{
        //    var jobs = await _context.jobs
        //        .Where(j => j.customerId == customerId)
        //        .ToListAsync();

        //    return Ok(jobs);
        //}
        [HttpGet("customer/{customerId}")]
        public async Task<IActionResult> GetJobsByCustomer(int customerId)
        {
            var jobs = await _context.jobs
                .Include(jobs => jobs.customer)
                .Include(jobs => jobs.tasker)
                .Include(jobs=> jobs.service)
                .Where(j => j.customerId == customerId)
                .Select(jobs=>new JobResponseDto{
                taskId=jobs.taskId,
                title=jobs.title,
                description=jobs.description,
                status = jobs.status.ToString(),
                scheduledDate
                =jobs.scheduledDate,    
                customerName=jobs.customer.fullName,
                taskerName=jobs.tasker !=null?jobs.tasker.fullName:"",
             serviceName=jobs.service.name

            })
                .ToListAsync();

            return Ok(jobs);
        }
        // ================================
        // ✅ GET JOBS BY TASKER
        // ================================
        //[HttpGet("tasker/{taskerId}")]
        //public async Task<IActionResult> GetJobsByTasker(int taskerId)
        //{
        //    var jobs = await _context.jobs
        //        .Where(j => j.taskerId == taskerId)
        //        .ToListAsync();

        //    return Ok(jobs);
        //}
        [HttpGet("tasker/{taskerId}")]
        public async Task<IActionResult> GetJobsByTasker(int taskerId)
        {
            var jobs = await _context.jobs
                .Include(j => j.customer)
                .Include(j => j.tasker)
                .Include(j => j.service)
                .Where(j => j.taskerId == taskerId)
                .Select(j => new JobResponseDto
                {
                    taskId = j.taskId,
                    title = j.title,
                    description = j.description,
                    status = j.status.ToString(),
                    scheduledDate = j.scheduledDate,
                    customerName = j.customer.fullName,
                    taskerName = j.tasker != null ? j.tasker.fullName : "",
                    serviceName = j.service.name
                })
                .ToListAsync();

            return Ok(jobs);
        }
        // ================================
        // ✅ UPDATE JOB STATUS
        // ================================
        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] taskStatus status)
        {
            var job = await _context.jobs.FindAsync(id);

            if (job == null)
                return NotFound();

            job.status = status;

            await _context.SaveChangesAsync();

            return Ok(new { message = "Job status updated" });
        }

        // ================================
        // ✅ DELETE JOB
        // ================================
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJob(int id)
        {
            var job = await _context.jobs.FindAsync(id);

            if (job == null)
                return NotFound();

            _context.jobs.Remove(job);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Job deleted successfully" });
        }
    

}
}
