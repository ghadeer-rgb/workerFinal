namespace workerFinal.DTO
{
    public class CreateJobDto
    {
      
            public int customerId { get; set; }
            public int taskerId { get; set; }
            public int serviceId { get; set; }

            public string title { get; set; } = string.Empty;
            public string description { get; set; } = string.Empty;
            public string address { get; set; } = string.Empty;
            public string city { get; set; } = string.Empty;

            public DateTime scheduledDate { get; set; }
        }

    
}
