namespace workerFinal.DTO
{
    public class TaskerProfileResponseDto
    {
        public int taskerId { get; set; }
        public string taskerName { get; set; } = string.Empty;
        public int serviceId { get; set; }
        public string serviceName { get; set; } = string.Empty;
        public string bio { get; set; } = string.Empty;
        public int experienceYears { get; set; }
        public decimal hourlyRate { get; set; }
        public string city { get; set; } = string.Empty;
        public double ratingAverage { get; set; }
        
        
        public bool isApproved { get; set; }


        public string? profileImage { get; set; }
    }
}
