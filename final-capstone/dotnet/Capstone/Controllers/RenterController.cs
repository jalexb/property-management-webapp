﻿using Capstone.DAO;
using Capstone.DAO.Renter;
using Capstone.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RenterController : Controller
       {
        private readonly IRenterDAO renterDAO;
        public RenterController(IRenterDAO renterService)
        {
            renterDAO = renterService; 
        }
        [HttpPost("/renter")]
        public IActionResult SaveRenter([FromBody] RenterInformation request)
        {


            int rowsAffected = renterDAO.AddUserInformation(request);

            if (rowsAffected == 1)
            {
                return Ok();
            }
            return BadRequest(new { Message = "An error occurred and renter was not saved" });
        }

        [HttpGet("/renter/{id}")]
        public IActionResult getRenterInformation(int id)
        {
            BasicRenterInformation renter_info = renterDAO.GetRenterInformation(id);
            renter_info.Property_Id = renterDAO.GetRenterPropertyIdFromLease(id);
            renter_info = renterDAO.GetRenterAddress(renter_info);

            return Ok(renter_info);
        }
    }
}
