using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace workerFinal.Models
{
    public class Review
    {
        public int reviewId { get; set; }

        public int taskId { get; set; }

        public int customerId { get; set; }

        public int taskerId { get; set; }

        public int rating { get; set; }  // 1 - 5

        public string comment { get; set; } = string.Empty;

        public DateTime createdAt { get; set; } = DateTime.UtcNow;

        // Navigation
        public Job task { get; set; } = null!;

        public User customer { get; set; } = null!;

        public User tasker { get; set; } = null!;
    }
}
