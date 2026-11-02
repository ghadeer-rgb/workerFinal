namespace workerFinal.DTO
{
    public class TaskerPublicProfileDto
    {
        
            public int taskerId { get; set; }
            public string taskerName { get; set; } = string.Empty;
            public string serviceName { get; set; } = string.Empty;
            public string bio { get; set; } = string.Empty;
            public int experienceYears { get; set; }
            public decimal hourlyRate { get; set; }
            public string city { get; set; } = string.Empty;
            public double ratingAverage { get; set; }
        public string? profileImageUrl { get; set; }
    }
    }



