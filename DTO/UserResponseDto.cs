namespace workerFinal.DTO
{
    public class UserResponseDto
    {
        public int userId { get; set; }
        public string fullName { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
        public string phone { get; set; } = string.Empty;
        public string role { get; set; } = string.Empty;
        public bool isActive { get; set; }
        public DateTime createdAt { get; set; }

        public string? profileImage { get; set; }
    }
}
