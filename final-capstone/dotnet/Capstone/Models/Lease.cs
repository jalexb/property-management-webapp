using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{



    public class PendingLease
    {
        public int Pending_Id { get; set; }
        public int User_Id { get; set; }
        public int Property_Id { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

        //Pending_Id, User_Id, Property_Id, FromDate, ToDate
    }


    public class PendingLeaseAndRenterInformation
    {
        public int Pending_Id { get; set; }
        public int User_Id { get; set; }
        public int Property_Id { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int Renter_Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Lease_Type { get; set; }
        public string Employment_History { get; set; }
        public decimal Salary { get; set; }
    }

    public class Lease
    {
        public int Lease_Id { get; set; }
        public DateTime Fromdate { get; set; }
        public DateTime Todate { get; set; }
        public int Userid { get; set; }
        public int Propertyid { get; set; }





    }

}
