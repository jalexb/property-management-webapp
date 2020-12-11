﻿using System;
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
             

        private readonly ILeaseDAO _leaseService;
        public LeaseController(ILeaseDAO leaseService)
        {
            _leaseService = leaseService;
        }

        [HttpPost("/lease")]
        public IActionResult SaveApplication([FromBody]PendingLease lease)
        {

           
            int rowsAffected = _leaseService.AddPendingLease(lease);
            
            if (rowsAffected >= 1)
            {
                return Ok();
            }
            return BadRequest(new { Message = "An error occurred and application was not saved" });
        }

        [HttpGet("/pendingApplications")]
        public IActionResult GetPendingApplications(int id)
        {
            return Ok();
        }
    }
}
