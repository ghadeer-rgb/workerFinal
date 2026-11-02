using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace workerFinal.Models
{
    public class Service
    {
        public int serviceId { get; set; }

        public string name { get; set; } = string.Empty;

        public string description { get; set; } = string.Empty;

        public string? icon { get; set; }

        public bool isActive { get; set; } = true;

        // Navigation
        public ICollection<Job> tasks { get; set; } = new List<Job>();

        public ICollection<TaskerProfile> taskerProfiles { get; set; } = new List<TaskerProfile>();
    }
}
