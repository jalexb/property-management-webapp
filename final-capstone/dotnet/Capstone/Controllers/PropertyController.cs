using Microsoft.AspNetCore.Mvc;
using Capstone.DAO;
using Capstone.Models;
using System.Collections.Generic;
using Capstone.DAO.Lease;
using System.Linq;

namespace Capstone.Controllers { 

    [Route("[controller]")]
    [ApiController]
    public class PropertyController : Controller
    {
        private readonly IPropertyDAO propertyDAO;
        private readonly ILeaseService leaseService;  
        public PropertyController(IPropertyDAO _propDAO, ILeaseService _leaseService) 
        {
            propertyDAO = _propDAO;
            leaseService = _leaseService;
        }

        [HttpGet]
        public IActionResult GetAvailableProperties()
        {
            IActionResult result;

            List<PropertyAndAddress> properties = propertyDAO.getProperties();
            List<int> unavailablePropertyIds = leaseService.GetUnavailablePropertyIds();
            if(properties.Count <= 0)
            {
                //wasn't able to get list of properties
                result = NoContent();
            } 
            else
            {
                //got list of available properties
                var availableProperties = properties.Where(p => !unavailablePropertyIds.Contains((int)p.propertyId)).ToList();
                result = Ok(availableProperties);
            }

            return result;
        }

        [HttpGet("{id}")]
        public IActionResult GetPropertyById(int id)
        {
            IActionResult result;
            PropertyAndAddress property = propertyDAO.GetProperty(id);

            if(property == null)
            {
                result = NoContent();
            }
            else
            {
                result = Ok(property);
            }

            return result;
        }



        //this method is for getting the rent_due for a Transaction Model
        [HttpGet("price/{property_id}")]
        public decimal GetPropertyPrice(int property_id)
        {

            decimal price = propertyDAO.GetPropertyPrice(property_id);

            return price;
        }
    }
}
