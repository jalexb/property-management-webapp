using AutoMapper;
using Capstone.Entities;
using Capstone.Models;
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
