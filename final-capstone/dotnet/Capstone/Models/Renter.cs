using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class RenterInformation
    {
        public int Renter_Id { get; set; }
        public int User_Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Lease_Type { get; set; }
        public decimal Salary { get; set; }


        //Lease_Id, Property_id, User_Id, FirstName, LastName, Address, PhoneNumber, Email, Lease_Type, Employment_History, Salary, FromDate, ToDate
    }
}
