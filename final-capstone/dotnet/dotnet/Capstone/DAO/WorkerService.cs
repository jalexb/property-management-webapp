using AutoMapper;
using Capstone.Controllers;
using Capstone.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.DAO
{
    public class WorkerService : IWorkerService
    {
        private readonly final_capstoneContext _dbContext;
        private readonly IMapper _mapper;
        public WorkerService(final_capstoneContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public List<WorkerInformation> GetWorkers()
        {
            return _dbContext.WorkerInformation.ToList();
        }

    }
}
