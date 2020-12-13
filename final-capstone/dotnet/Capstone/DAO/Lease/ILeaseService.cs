using Capstone.Models;
using System.Collections.Generic;

namespace Capstone.DAO.Lease
{
    public interface ILeaseService
    {
        bool SavePendingLease(LeaseRequest lease);
        bool IsDupilcateLease(PendingLease lease);
        List<int> GetUnavailablePropertyIds();
        List<LeaseResponse> GetCompletedApplications();
    }
}