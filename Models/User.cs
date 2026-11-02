using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace workerFinal.Models
{
    public enum userRole
    {

        customer = 0,
        tasker = 1,
        admin = 2

    }
    public class User
    {
        public int userId { get; set; }
        public string fullName { get; set; }
        public string email { get; set; }

        public string passwordHash { get; set; }

        public string phone { get; set; } = string.Empty;

        public userRole role { get; set; }

        public DateTime createdAt { get; set; } = DateTime.UtcNow;

        public bool isActive { get; set; } = true;

        public string? profileImage { get; set; }

        // Navigation
        public TaskerProfile? taskerProfile { get; set; }

        public ICollection<Job> customerTasks { get; set; } = new List<Job>();

        public ICollection<Job> assignedTasks { get; set; } = new List<Job>();

        public ICollection<Review> reviewsGiven { get; set; } = new List<Review>();

        public ICollection<Review> reviewsReceived { get; set; } = new List<Review>();

        public ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();



        // For Favorites where the user is the tasker
        public ICollection<Favorite> FavoritedBy { get; set; } = new List<Favorite>();
    }
}
