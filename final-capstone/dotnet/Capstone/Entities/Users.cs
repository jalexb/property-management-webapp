using System;
using System.Collections.Generic;

namespace Capstone.Entities
{
    public partial class Users
    {
        public Users()
        {
            AddressTable = new HashSet<AddressTable>();
            Lease = new HashSet<Lease>();
            PendingLeases = new HashSet<PendingLeases>();
            RenterInformation = new HashSet<RenterInformation>();
        }

        public int UserId { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Salt { get; set; }
        public string UserRole { get; set; }

        public virtual ICollection<AddressTable> AddressTable { get; set; }
        public virtual ICollection<Lease> Lease { get; set; }
        public virtual ICollection<PendingLeases> PendingLeases { get; set; }
        public virtual ICollection<RenterInformation> RenterInformation { get; set; }
    }
}
