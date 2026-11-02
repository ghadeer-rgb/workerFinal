using workerFinal.Models;

namespace workerFinal.DTO
{
    public class CreateUserDto
    {
        public string fullName { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
        public string phone { get; set; } = string.Empty;

        public string passwordHash { get; set; } = string.Empty;
        public userRole role { get; set; } // customer=0, tasker=1, admin=2
        public bool isActive { get; set; } = true;
    }
}
