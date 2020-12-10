using System;
using System.Collections.Generic;

namespace Capstone.Entities
{
    public partial class Lease
    {
        public int LeaseId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public int UserId { get; set; }
        public int PropertyId { get; set; }

        public virtual Properties Property { get; set; }
        public virtual Users User { get; set; }
    }
}
