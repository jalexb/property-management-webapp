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
             
    }
}
