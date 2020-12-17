using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.DAO.Maintenance
{
    public interface IMaintenanceDAO
    {
        int AddMaintenanceTicket(MaintenanceTicketRequest ticket);
        List<TicketAndAddress> GetAssignedTicketsByUserId(int userId);
        int MarkTicketCompleted(int request_Id, TicketAndAddress ticket);
    }
}
