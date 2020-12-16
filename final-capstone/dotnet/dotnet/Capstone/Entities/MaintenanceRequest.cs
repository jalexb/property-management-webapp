using System;
using System.Collections.Generic;

namespace Capstone.Entities
{
    public partial class MaintenanceRequest
    {
        public int RequestId { get; set; }
        public int RenterId { get; set; }
        public int? WorkerId { get; set; }
        public string RequestInfo { get; set; }
        public int PropertyId { get; set; }
        public bool? IsAssigned { get; set; }
        public bool? IsFixed { get; set; }
        public string PostFixReport { get; set; }

        public virtual Properties Property { get; set; }
        public virtual Users Renter { get; set; }
        public virtual Users Worker { get; set; }
    }
}
