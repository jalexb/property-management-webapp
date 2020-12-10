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
             

        private readonly ILeaseService _leaseService;
        public LeaseController(ILeaseService leaseService)
        {
            _leaseService = leaseService;
        }

        [HttpPost("/lease")]
        public IActionResult SaveApplication([FromBody]PendingLeasesRequest lease)
        {

           
            bool lease_result = _leaseService.SavePendingLease(lease);
            
            if (lease_result)
            {
                return Ok();
            }
            return BadRequest(new { Message = "An error occurred and application was not saved" });
        }
    }
}
