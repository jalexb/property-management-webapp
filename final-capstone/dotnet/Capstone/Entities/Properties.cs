using System;
using System.Collections.Generic;

namespace Capstone.Entities
{
    public partial class Properties
    {
        public Properties()
        {
            Lease = new HashSet<Lease>();
        }

        public int PropertyId { get; set; }
        public int LandlordId { get; set; }
        public int AddressId { get; set; }
        public int Bedrooms { get; set; }
        public int Bathrooms { get; set; }
        public string Photo { get; set; }
        public string PropDesc { get; set; }
        public decimal Price { get; set; }

        public virtual AddressTable Address { get; set; }
        public virtual Landlord Landlord { get; set; }
        public virtual ICollection<Lease> Lease { get; set; }
    }
}
