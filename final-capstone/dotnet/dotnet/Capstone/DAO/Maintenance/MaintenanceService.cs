using AutoMapper;
using Capstone.DAO.Maintenance;
using Capstone.Entities;
using Capstone.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.DAO.Lease
{
    public class MaintenanceService : IMaintenanceService
    {
        private readonly final_capstoneContext _dbContext;
        private readonly IMapper _mapper;
        public MaintenanceService(final_capstoneContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public bool AddMaintenanceTicket(MaintenanceTicketRequest ticket)
        {
            try
            {
                var maintenanceRequest = _mapper.Map<Entities.MaintenanceRequest>(ticket);
                _dbContext.MaintenanceRequest.Add(maintenanceRequest);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
           
        }

        public bool AssignMaintenanceTicket(MaintenanceTicketRequest ticket)
        {
            try
            {
                var maintenanceRequest = _dbContext.MaintenanceRequest.Where(r => r.RequestId == ticket.RequestId).FirstOrDefault();
                maintenanceRequest.WorkerId = ticket.WorkerId;
                maintenanceRequest.IsAssigned = true;
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public List<MaintenanceResponse> GetUnassignedMaintenanceTickets()
        {
            var tickets = _dbContext.MaintenanceRequest.Include(i => i.Property).ThenInclude(p => p.Address)
                .Where(u=>u.IsAssigned != true).ToList();
            var maintenanceTickets = _mapper.Map<List<MaintenanceResponse>>(tickets);
            foreach (var item in maintenanceTickets)
            {
                var renterInformation = _dbContext.RenterInformation.Where(r => r.UserId == item.RenterId).FirstOrDefault();
                item.RenterInformation = _mapper.Map<RenterInformationResponse>(renterInformation);
            }
            return maintenanceTickets;
        }

    }
}
