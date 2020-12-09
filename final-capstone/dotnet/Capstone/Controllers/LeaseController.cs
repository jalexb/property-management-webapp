using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.DAO;
using Capstone.Models;
using Microsoft.AspNetCore.Mvc;

namespace Capstone.Controllers
{
    public class LeaseController : Controller
    {
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
    }
}
