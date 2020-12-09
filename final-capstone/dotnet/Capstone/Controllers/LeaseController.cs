using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.DAO;
using Capstone.Models;
using Microsoft.AspNetCore.Mvc;

namespace Capstone.Controllers
{

    [Route("[controller]")]
    [ApiController]
    public class LeaseController : Controller
    {
<<<<<<< HEAD

        private readonly ILeaseDAO leaseDAO;
        public LeaseController(ILeaseDAO _leaseDAO)
        {
            leaseDAO = _leaseDAO;
        }


        [HttpPost("{pending_lease}")] //localhost:43545/lease/{pending_lease_object}
        public IActionResult addPendingLease(PendingLease lease)
        {
            int rowCount = leaseDAO.AddPendingLease(lease);

            if(rowCount == 1)
            {
                return Ok();
            }
            else
            {
                return NoContent();
            }
        }
             
=======
        private readonly ILeaseDAO _leaseDAO;
        public LeaseController(ILeaseDAO leaseDAO)
        {
            _leaseDAO = leaseDAO;
        }

        [HttpPost("/lease")]
        public IActionResult SaveApplication([FromBody]Lease lease)
        {
            var result = _leaseDAO.SaveApplication(lease);
            if (result)
            {
                return Created(lease.Firstname, null);
            }
            return BadRequest(new { Message = "An error occurred and application was not saved" });
        }
>>>>>>> 443074b6524f4002f4cb155653cc3fc989615801
    }
}
