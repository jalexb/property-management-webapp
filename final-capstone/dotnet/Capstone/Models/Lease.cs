
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
        public Lease Pending_Lease { get; set; }
        public BasicRenterInformation Renter_Info { get; set; }
    }

    public class Lease
    {
        public int Lease_Id { get; set; }
        public DateTime From_Date { get; set; }
        public DateTime To_Date { get; set; }
        public int User_Id { get; set; }
        public int Property_Id { get; set; }
        public string Lease_Type { get; set; }
        //Lease_Id, From_Date, To_Date, User_Id, Property_Id, Lease_Type
    }

}
