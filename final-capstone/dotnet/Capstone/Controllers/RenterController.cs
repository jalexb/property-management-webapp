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
        private readonly IRenterService _renterService;
        public RenterController(IRenterService renterService)
        {
            _renterService = renterService; 
        }
        [HttpPost("/renter")]
        public IActionResult SaveRenter([FromBody] RenterInformationRequest request)
        {


            bool lease_result = _renterService.SaveRenter(request);

            if (lease_result)
            {
                return Ok();
            }
            return BadRequest(new { Message = "An error occurred and renter was not saved" });
        }
    }
}
