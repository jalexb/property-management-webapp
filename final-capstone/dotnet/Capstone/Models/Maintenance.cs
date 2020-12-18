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
        public int? UserId { get; set; }
        public string Request_Info { get; set; }
        public int Property_Id { get; set; }

        //Renter_Id, UserId, Request_Info, Property_Id
    }

    public class TicketAndAddress 
    {
        public int Request_Id { get; set; }
        public int Renter_Id { get; set; }
        public int? Worker_Id { get; set; }
        public string Request_Info { get; set; }
        public int Property_Id { get; set; }
        public bool Is_Assigned { get; set; }
        public bool? Is_Fixed { get; set; }
        public string Post_Fix_Report { get; set; }
        public string Street { get; set; }
        public string Street2 { get; set; }
        
    }

}
