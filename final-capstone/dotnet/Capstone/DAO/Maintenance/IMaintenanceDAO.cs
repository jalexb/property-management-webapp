using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.DAO.Maintenance
{
    public interface IMaintenanceDAO
    {
        int AddMaintenanceTicket(MaintenanceTicket ticket);
    }
}
