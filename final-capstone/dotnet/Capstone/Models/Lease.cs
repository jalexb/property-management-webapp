using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class Lease
    {
        public int Lease_Id { get; set; }
        public DateTime From_Date { get; set; }
        public DateTime To_Date { get; set; }
        public int User_Id { get; set; }
        public int Property_Id { get; set; }
    }

    public class PendingLeases
    {
        public int Lease_Id { get; set; }
        public int User_Id { get; set; }
    }
}
