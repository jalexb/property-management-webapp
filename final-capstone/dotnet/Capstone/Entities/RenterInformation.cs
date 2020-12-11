using System;
using System.Collections.Generic;

namespace Capstone.Entities
{
    public partial class RenterInformation
    {
        public int RenterId { get; set; }
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string LeaseType { get; set; }
        public decimal Salary { get; set; }

        public virtual Users User { get; set; }
    }
}
