using Capstone.Models;
using System.Collections.Generic;

namespace Capstone.DAO.Maintenance
{
    public interface IMaintenanceService
    {
        List<MaintenanceResponse> GetUnassignedMaintenanceTickets();
        bool AddMaintenanceTicket(MaintenanceTicketRequest ticket);
        bool AssignMaintenanceTicket(MaintenanceTicketRequest ticket);
    }
}