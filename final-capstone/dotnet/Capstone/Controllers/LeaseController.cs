using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.DAO;
using Capstone.DAO.Lease;
using Capstone.Models;
using Microsoft.AspNetCore.Mvc;

namespace Capstone.Controllers
{

    [Route("[controller]")]
    [ApiController]
    public class LeaseController : Controller
    {
             

        private readonly ILeaseDAO _leaseDAO;
        private readonly ILeaseService _leaseService;
        public LeaseController(ILeaseDAO leaseDAO, ILeaseService leaseService)
        {
            _leaseDAO = leaseDAO;
            _leaseService = leaseService;
        }

        [HttpPost("/lease")]
        public IActionResult SaveApplication([FromBody]PendingLease lease)
        {

           
            int rowsAffected = _leaseDAO.AddPendingLease(lease);
            
            if (rowsAffected >= 1)
            {
                return Ok();
            }
            return BadRequest(new { Message = "An error occurred and application was not saved" });
        }

        [HttpGet]
        [Route("/pendingApplications/{id:int}")]
        public IActionResult GetPendingApplications(int id)
        {
            List<LeaseResponse> leaseResponses = _leaseService.GetPendingApplicationsByUserId(id);

            return Ok(leaseResponses);
        }
    }
}
