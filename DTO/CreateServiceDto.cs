namespace workerFinal.DTO
{
    public class CreateServiceDto
    {
        public string name { get; set; } = string.Empty;
        public string description { get; set; } = string.Empty;
        public string? icon { get; set; }
        public bool isActive { get; set; } = true;
    }
}
