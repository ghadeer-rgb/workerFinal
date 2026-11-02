namespace workerFinal.Models
{
    public class Favorite
    {
        
            public int id { get; set; }
            public int userId { get; set; }
        public User user { get; set; } = null!;
        public int taskerId { get; set; }
        public User tasker { get; set; } = null!;
    }
}
