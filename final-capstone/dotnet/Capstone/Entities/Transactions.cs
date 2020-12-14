using System;
using System.Collections.Generic;

namespace Capstone.Entities
{
    public partial class Transactions
    {
        public int TransactionId { get; set; }
        public int LeaseId { get; set; }
        public int PropertyId { get; set; }
        public DateTime PaymentDueDate { get; set; }
        public decimal LateFees { get; set; }
        public bool Paid { get; set; }
        public decimal AmountPaid { get; set; }

        public virtual Lease Lease { get; set; }
        public virtual Properties Property { get; set; }
    }
}
