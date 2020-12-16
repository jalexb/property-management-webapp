using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class LeaseRequest
    {
        public int PendingId { get; set; }
        public int UserId { get; set; }
        public int PropertyId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public bool? IsApproved { get; set; }
    }
}
