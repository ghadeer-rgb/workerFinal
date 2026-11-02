using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace workerFinal.Models
{
    public enum taskStatus
    {
        Pending = 0,
        Assigned = 1,
        Completed = 2,
        Cancelled = 3
    }
    public class Job
    {
        public int taskId { get; set; }

       
        public int customerId { get; set; }
       
        public int? taskerId { get; set; }

        public int serviceId { get; set; }

        public string title { get; set; } = string.Empty;

        public string description { get; set; } = string.Empty;

        public string address { get; set; } = string.Empty;

        public string city { get; set; } = string.Empty;

        public DateTime scheduledDate { get; set; }

        public taskStatus status { get; set; } = taskStatus.Pending;

        public DateTime createdAt { get; set; } = DateTime.UtcNow;

        // Navigation
        public User customer { get; set; } = null!;

        public User? tasker { get; set; }

        public Service service { get; set; } = null!;

        public Review? Review { get; set; }

    
}
}
