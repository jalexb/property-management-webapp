using AutoMapper;
using Capstone.Entities;
using Capstone.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.DAO.Lease
{
    public class LeaseService : ILeaseService
    {
        private readonly final_capstoneContext _dbContext;
        private readonly IMapper _mapper;
        public LeaseService(final_capstoneContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<LeaseResponse> GetCompletedApplications()
        {
            var leases = _dbContext.Lease.Include(i => i.Property).ThenInclude(p => p.Address)
                .ToList();
            var leaseResponses =  _mapper.Map<List<LeaseResponse>>(leases);
            foreach (var item in leaseResponses)
            {
                var renterInformation = _dbContext.RenterInformation.Where(r => r.UserId == item.UserId).FirstOrDefault();
                item.RenterInformation = _mapper.Map<RenterInformationResponse>(renterInformation);
            }
            return leaseResponses;
        }

        public List<LeaseResponse> GetPendingApplicationsByUserId(int id)
        {
            throw new NotImplementedException();
        }

        public List<int> GetUnavailablePropertyIds()
        {
            return _dbContext.Lease.Where(l => l.CurrentStatus == "Approved").Select(l => l.PropertyId).ToList();
        }

        public bool IsDupilcateLease(PendingLease lease)
        {
            // checks to make sure the user hasn't applied for a lease on the same property
            return _dbContext.Lease.Any(l => l.UserId == lease.UserId && l.PropertyId == lease.PropertyId);
        }

        public bool SavePendingLease(LeaseRequest lease)
        {
            try
            {
                var pendingLeases = _mapper.Map<Entities.Lease>(lease);
                _dbContext.Lease.Add(pendingLeases);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
