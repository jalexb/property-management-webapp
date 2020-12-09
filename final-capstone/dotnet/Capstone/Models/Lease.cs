using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{

    public class PendingLeases
    {
        public int Lease_Id { get; set; }
        public int User_Id { get; set; }
    }

    public class Lease
    {
        public int Lease_Id { get; set; }
        public DateTime Fromdate { get; set; }
        public DateTime Todate { get; set; }
        public int Userid { get; set; }
        public int Propertyid { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Phonenumber { get; set; }
        public string Email { get; set; }
        public string Dob { get; set; }
        public bool isapproved { get; set; }





    }

}
