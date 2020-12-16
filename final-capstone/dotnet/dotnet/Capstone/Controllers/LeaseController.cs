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


        private readonly ILeaseDAO _leaseDAO;
        private readonly ILeaseService _leaseService;
        private readonly IRenterDAO _renterDAO;
        public LeaseController(ILeaseDAO leaseDAO, ILeaseService leaseService, IRenterDAO renterDAO, IPropertyDAO propertyDAO)
        {
            _leaseDAO = leaseDAO;
            _leaseService = leaseService;
            _renterDAO = renterDAO;
        }

        [HttpPost("/lease")]
        public IActionResult SaveApplication([FromBody] PendingLease lease)
        {
            if (_leaseService.IsDupilcateLease(lease))
            {
                return BadRequest(new { Message = "An error occurred - user has already applied for a lease on this property" });
            }

            int rowsAffected = _leaseDAO.AddPendingLease(lease);

            if (rowsAffected >= 1)
            {
                return Ok();
            }
            return BadRequest(new { Message = "An error occurred and application was not saved" });
        }

        [HttpGet]
        [Route("/pendingApplications/{id:int}")]
        public IActionResult GetPendingApplications(int id)
        {
            List<LeaseResponse> leaseResponses = _leaseService.GetPendingApplicationsByUserId(id);

            return Ok(leaseResponses);
        }
        [HttpPost("{property_id}/{user_id}")]
        public IActionResult HasUserAppliedForLease(int user_id, int property_id)
        {
            bool result = _leaseDAO.CheckIfUserAppliedForThisProperty(user_id, property_id);

            if(result == true)
            {
                return Ok(true);
            }

            return Ok(false);



        }

        [HttpGet]
        [Route("/completedApplications")]
        public IActionResult GetCompletedApplications()
        {
            List<LeaseResponse> leaseResponses = _leaseService.GetCompletedApplications();

            return Ok(leaseResponses);
        }

        [HttpGet("/leases/{landlord_id}")]
        public IActionResult GetLandlordsLeases(int landlord_id)
        {
            List<Lease> leases = _leaseDAO.GetLandlordLeases(landlord_id);
            IActionResult result = BadRequest();

            if(leases != null)
            {
                List<LeaseAndRenterInformation> leaseAndRenterInfoList = new List<LeaseAndRenterInformation>();
                foreach(Lease lease in leases)
                {
                    BasicRenterInformation RenterInfo = _renterDAO.GetRenterInformation(lease.User_Id);
                    if (RenterInfo == null) 
                        return BadRequest();
                    
                    RenterInfo.Address = _renterDAO.GetRenterAddress(lease.Property_Id);
                   
                    if (RenterInfo.Address == null) 
                        return BadRequest();
                    
                    LeaseAndRenterInformation leaseAndRenterInfo = new LeaseAndRenterInformation();

                    leaseAndRenterInfo.Lease = lease;
                    leaseAndRenterInfo.RenterInfo = RenterInfo;

                    leaseAndRenterInfoList.Add(leaseAndRenterInfo);
                }
                result = Ok(leaseAndRenterInfoList);
            }

            return result;
            
        }

        [HttpPost("/lease/approve/{lease_id}")]
        public IActionResult ApproveLease(int lease_id)
        {
            IActionResult result = BadRequest();
            int rowsAffected = _leaseDAO.ApprovePendingLease(lease_id);
            if(rowsAffected == 1)
            {
                int? property_id = GetPropertyIdFromLeaseId(lease_id);
                if(property_id != null)
                {
                    RejectPendingLeasesWithPropertyId((int)property_id); //reject other leases with the same user Id when a lease is approved
                }
                result = NoContent();
            }

            return result;
        }

        private int? GetPropertyIdFromLeaseId(int lease_id)
        {
            return _leaseDAO.GetLease(lease_id)?.Property_Id; //if return value is null, return null, else return returnValue.property_id
        }

        private bool RejectPendingLeasesWithPropertyId(int property_id)
        {
            return _leaseDAO.RejectPendingLeasesWithPropertyId(property_id) > 0 ? true : false;
        }

        [HttpPost("/lease/reject/{lease_id}")]
        public IActionResult RejectLease(int lease_id)
        {
            IActionResult result = BadRequest();
            int rowsAffected = _leaseDAO.RejectPendingLease(lease_id);
            if (rowsAffected == 1)
            {
                result = Ok();
            }

            return result;
        }
    }
}
