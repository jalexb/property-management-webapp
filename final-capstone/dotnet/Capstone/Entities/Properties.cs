using System;
using System.Collections.Generic;

namespace Capstone.Entities
{
    public partial class Properties
    {
        public Properties()
        {
            Lease = new HashSet<Lease>();
            Transactions = new HashSet<Transactions>();
        }

        public int PropertyId { get; set; }
        public int UserId { get; set; }
        public int AddressId { get; set; }
        public int Bedrooms { get; set; }
        public int Bathrooms { get; set; }
        public string Photo { get; set; }
        public string PropDesc { get; set; }
        public decimal Price { get; set; }

        public virtual AddressTable Address { get; set; }
        public virtual Users User { get; set; }
        public virtual ICollection<Lease> Lease { get; set; }
        public virtual ICollection<Transactions> Transactions { get; set; }
    }
}
