using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.DAO.Maintenance;
using Capstone.Models;
using Microsoft.AspNetCore.Mvc;

namespace Capstone.Controllers
{
    public class MaintenanceController : Controller
    {

        private readonly IMaintenanceDAO maintenanceDAO;
        public MaintenanceController(IMaintenanceDAO _maintenanceDAO)
        {
            maintenanceDAO = _maintenanceDAO;
        }

        //add maintenance ticker to maintenance_request table (Renter_Id, UserId, Request_Info, Property_Id)
        [HttpPost("submit/ticket")]
        public IActionResult AddMaintenanceTicket([FromBody] MaintenanceTicket ticket)
        {
            int rowsAffected = maintenanceDAO.AddMaintenanceTicket(ticket);

            if(rowsAffected == 1)
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpGet("maintenance/assigned/{userId}")]
        public IActionResult GetAssignedTicketsByUserId(int userId)
        {
            IActionResult result = BadRequest();
            List<TicketAndAddress> tickets = new List<TicketAndAddress>();

            tickets = maintenanceDAO.GetAssignedTicketsByUserId(userId);

            if(tickets != null)
            {
                result = Ok(tickets);
            }

            return result;
        }

        [HttpPut("maintenance/tickets/{request_Id}")]
        public IActionResult MarkTicketCompleted(int request_Id, TicketAndAddress ticket)
        {
            IActionResult result = BadRequest();
            int rowsAffected = maintenanceDAO.MarkTicketCompleted(request_Id, ticket);

            if(rowsAffected > 0)
            {
                result = Ok();
            }

            return result;
        }

    }
}
