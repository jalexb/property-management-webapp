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
        public IActionResult SaveApplication([FromBody]PendingLeaseAndRenterInformation lease)
        {

            PendingLease pending_lease = new PendingLease();
            RenterInformation info = new RenterInformation();

            info.Address = lease.Address;
            info.Email = lease.Email;
            info.FirstName = lease.FirstName;
            info.LastName = lease.LastName;
            info.Lease_Type = lease.Lease_Type;
            info.PhoneNumber = lease.PhoneNumber;
            info.Salary = lease.Salary;
            info.User_Id = lease.User_Id;

            pending_lease.FromDate = lease.FromDate;
            pending_lease.ToDate = lease.ToDate;
            pending_lease.User_Id = lease.User_Id;
            pending_lease.Property_Id = lease.Property_Id;

            int lease_result = _leaseDAO.AddPendingLease(pending_lease);
            int renterInfo_result = _leaseDAO.AddUserInformation(info);

            if (lease_result == 1 && renterInfo_result == 1)
            {
                return Ok();
            }
            return BadRequest(new { Message = "An error occurred and application was not saved" });
        }
    }
}
