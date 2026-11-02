namespace workerFinal.DTO
{
    public class ReviewResponseDto
    {
        public int reviewId { get; set; }
        public int taskId { get; set; }
        public int customerId { get; set; }
        public int taskerId { get; set; }
        public int rating { get; set; }
        public string comment { get; set; } = string.Empty;
        public DateTime createdAt { get; set; }

        public string customerName { get; set; } = string.Empty;
        public string taskerName { get; set; } = string.Empty;
    }
}
