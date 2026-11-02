using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace workerFinal.DTO
{
    public class CreateReviewDto
    {
        public int taskId { get; set; }
        public int customerId { get; set; }
        public int taskerId { get; set; }
        public int rating { get; set; } // 1-5
        public string comment { get; set; } = string.Empty;
    }
}
