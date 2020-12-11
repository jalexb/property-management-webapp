using System;
using System.Collections.Generic;

namespace Capstone.Entities
{
    public partial class Landlord
    {
        public Landlord()
        {
            Properties = new HashSet<Properties>();
        }

        public int LandlordId { get; set; }
        public int UserId { get; set; }

        public virtual Users User { get; set; }
        public virtual ICollection<Properties> Properties { get; set; }
    }
}
