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
        
        public List<LeaseResponse> GetPendingApplicationsByUserId(int id)
        {
            var landlordId = _dbContext.Landlord.Where(i => i.UserId == id).Select(i=>i.LandlordId).FirstOrDefault();
            var propertyIds = _dbContext.Properties.Where(i => i.LandlordId == landlordId).Select(i => i.PropertyId).ToList();
            var leases = _dbContext.Lease.Include(i=>i.Property)
                .Where(i => propertyIds.Contains(i.PropertyId) && i.CurrentStatus == "pending").ToList();
            return _mapper.Map<List<LeaseResponse>>(leases);
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
