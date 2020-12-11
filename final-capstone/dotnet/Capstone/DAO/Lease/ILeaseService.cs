using Capstone.Models;
using System.Collections.Generic;

namespace Capstone.DAO.Lease
{
    public interface ILeaseService
    {
        bool SavePendingLease(LeaseRequest lease);
        List<LeaseResponse> GetPendingApplicationsByUserId(int id);
    }
}