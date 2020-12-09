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
             

        private readonly ILeaseDAO _leaseDAO;
        public LeaseController(ILeaseDAO leaseDAO)
        {
            _leaseDAO = leaseDAO;
        }

        [HttpPost("/lease")]
        public IActionResult SaveApplication([FromBody]PendingLease lease)
        {
            int result = _leaseDAO.AddPendingLease(lease);
            if (result == 1)
            {
                return Ok();
            }
            return BadRequest(new { Message = "An error occurred and application was not saved" });
        }
    }
}
