using Capstone.Models;

namespace Capstone.DAO.Lease
{
    public interface ILeaseService
    {
        bool SavePendingLease(PendingLeasesRequest lease);
    }
}