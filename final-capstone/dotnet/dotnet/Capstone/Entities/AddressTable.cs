using System;
using System.Collections.Generic;

namespace Capstone.Entities
{
    public partial class AddressTable
    {
        public AddressTable()
        {
            Properties = new HashSet<Properties>();
        }

        public int AddressId { get; set; }
        public int UserId { get; set; }
        public string PropertyType { get; set; }
        public string Street { get; set; }
        public string Street2 { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public int Zip { get; set; }

        public virtual Users User { get; set; }
        public virtual ICollection<Properties> Properties { get; set; }
    }
}
