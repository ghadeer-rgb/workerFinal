namespace workerFinal.DTO
{
    public class JobResponseDto
    {
        public int taskId { get; set; }
        public string title { get; set; } = string.Empty;
        public string description { get; set; } = string.Empty;
        public string status { get; set; } = string.Empty;
        public DateTime scheduledDate { get; set; }

        public string customerName { get; set; } = string.Empty;
        public string taskerName { get; set; } = string.Empty;
        public string serviceName { get; set; } = string.Empty;
    }

}
