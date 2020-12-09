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

    public class Lease
    {
        public int Lease_Id { get; set; }
        public DateTime Fromdate { get; set; }
        public DateTime Todate { get; set; }
        public int Userid { get; set; }
        public int Propertyid { get; set; }





    }

}
