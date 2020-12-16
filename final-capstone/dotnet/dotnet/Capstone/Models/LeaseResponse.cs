using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class LeaseResponse
    {
        public int LeaseId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public int UserId { get; set; }
        public int PropertyId { get; set; }
        public string CurrentStatus { get; set; }

        public virtual PropertiesResponse Property { get; set; }
        public virtual RenterInformationResponse RenterInformation { get; set; }
    }
}
