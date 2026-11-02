using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace workerFinal.Models
{
    public class TaskerProfile
    {
        public int taskerId { get; set; }   // PK & FK to User

        public int serviceId { get; set; }

        public string bio { get; set; } = string.Empty;

        public int experienceYears { get; set; }

        public decimal hourlyRate { get; set; }

        public string city { get; set; } = string.Empty;

        public double ratingAverage { get; set; } = 0;

        public bool isApproved { get; set; } = false;

        // Navigation
        public User tasker { get; set; } = null!;

        public Service service { get; set; } = null!;

        public string? profileImageUrl { get; set; }
    }
}
