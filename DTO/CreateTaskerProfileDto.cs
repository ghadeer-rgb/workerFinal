namespace workerFinal.DTO
{
    public class CreateTaskerProfileDto
    {
        public int taskerId { get; set; }   // FK to User
        public int serviceId { get; set; }
        public string bio { get; set; } = string.Empty;
        public int experienceYears { get; set; }
        public decimal hourlyRate { get; set; }
        public string city { get; set; } = string.Empty;
        public bool isApproved { get; set; } = false;
    }
}
