using Capstone.Models;

namespace Capstone.DAO.Lease
{
    public interface ILeaseService
    {
        bool SavePendingLease(LeaseRequest lease);
    }
}