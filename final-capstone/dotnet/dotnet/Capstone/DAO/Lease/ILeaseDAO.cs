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
        int ApprovePendingLease(int lease_id);
        Models.Lease GetLease(int lease_id);
        int RejectPendingLease(int lease_id);
        int RejectPendingLeasesWithPropertyId(int property_id);
        List<Models.Lease> GetLandlordLeases(int landlord_id);
        bool CheckIfUserAppliedForThisProperty(int userId, int property_id);
        Models.Lease GetAcceptedLeaseWithPropertyId(int property_id);
    }
}
