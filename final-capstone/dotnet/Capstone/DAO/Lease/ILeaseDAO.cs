using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.DAO
{
    public interface ILeaseDAO
    {
        int AddPendingLease(PendingLease lease);
        int ApprovePendingLease(Capstone.Models.Lease lease);
        int DeletePendingLease(int pending_id);
        int AddUserInformation(RenterInformation renter_info);
    }
}
