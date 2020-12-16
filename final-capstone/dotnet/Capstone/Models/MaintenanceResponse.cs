using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class MaintenanceResponse
    {
        public int RequestId { get; set; }
        public int RenterId { get; set; }
        public int? WorkerId { get; set; }
        public string RequestInfo { get; set; }
        public int PropertyId { get; set; }
        public bool? IsAssigned { get; set; }
        public bool? IsFixed { get; set; }
        public string PostFixReport { get; set; }

        public virtual PropertiesResponse Property { get; set; }
        public virtual UsersResponse Renter { get; set; }
        public virtual UsersResponse Worker { get; set; }
        public RenterInformationResponse RenterInformation { get; internal set; }
    }
}
