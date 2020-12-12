using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class Transaction
    {
        public int Transaction_Id { get; set; }
        public int Lease_Id { get; set; } 
        public int Property_Id { get; set; } 
        public DateTime Payment_Due_Date { get; set; } 
        public decimal Late_Fees { get; set; } 
        public decimal Paid { get; set; }
    }
}
