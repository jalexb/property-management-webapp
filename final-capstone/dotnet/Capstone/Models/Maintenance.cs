using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class MaintenanceTicket
    {
        public int Request_Id { get; set; }
        public int Renter_Id { get; set; }
        public int UserId { get; set; }
        public int Request_Info { get; set; }
        public int Property_Id { get; set; }

        //Renter_Id, UserId, Request_Info, Property_Id
    }
}
