using Capstone.DAO;
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
        private readonly IRenterService renterService;
        public RenterController(IRenterDAO _renterDAO, IRenterService _renterService)
        {
            renterDAO = _renterDAO;
            renterService = _renterService;
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
            if (renter_info.User_Id != 0)
            {
                renter_info.Property_Id = renterDAO.GetRenterPropertyIdFromLease(id);
                renter_info = renterDAO.GetRenterAddress(renter_info);
                return Ok(renter_info);
            }

            return BadRequest();

            
        }
        [HttpGet("/renterinfo/{id}")]
        public IActionResult getRenterInfo(int id)
        {
            RenterInformationResponse renterInformationResponse = renterService.GetRenterInfo(id);
            if (renterInformationResponse != null)
            {
               return Ok(renterInformationResponse);
            }

            return BadRequest();
        }

        [HttpPut("/renter")]
        public IActionResult updateRenter([FromBody]RenterInformationRequest request)
        {
            IActionResult result = BadRequest();

            bool isSuccess = renterService.UpdateRenter(request);

            if (isSuccess)
            {
                result = Ok();
            }
            return result;
        }

        [HttpPut("/renter/userRole/{id}")]
        public IActionResult setUserTorenter(int id)
        {
            IActionResult result = BadRequest();

            int rowsAffected = renterDAO.SetUserRoleToRenter(id);

            if(rowsAffected == 1)
            {
                result = Ok();
            }
            return result;
        }
    }
}
