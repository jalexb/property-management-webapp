
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{



    public class PendingLease
    {
        public int LeaseId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public int UserId { get; set; }
        public int PropertyId { get; set; }

        //Pending_Id, User_Id, Property_Id, FromDate, ToDate
    }


    public class PendingLeaseAndRenterInformation
    {
        public int Pending_Id { get; set; }
        public int User_Id { get; set; }
        public int Property_Id { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Lease_Type { get; set; }
        public decimal Salary { get; set; }
    }

    public class Lease
    {
        public int Lease_Id { get; set; }
        public DateTime From_Date { get; set; }
        public DateTime To_Date { get; set; }
        public int User_Id { get; set; }
        public int Property_Id { get; set; }
        //Lease_Id, From_Date, To_Date, User_Id, Property_Id
    }

}
