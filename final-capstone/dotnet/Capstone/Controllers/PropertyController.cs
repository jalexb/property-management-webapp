using Microsoft.AspNetCore.Mvc;
using Capstone.DAO;
using Capstone.Models;
using System.Collections.Generic;

namespace Capstone.Controllers { 

    [Route("[controller]")]
    [ApiController]
    public class PropertyController : Controller
    {
        private readonly IPropertyDAO propertyDAO;
        public PropertyController(IPropertyDAO _propDAO)
        {
            propertyDAO = _propDAO;
        }

        [HttpGet]
        public IActionResult GetAllProperties()
        {
            IActionResult result;

            List<PropertyAndAddress> properties = propertyDAO.getProperties();

            if(properties.Count <= 0)
            {
                //wasn't able to get list of properties
                result = NoContent();
            } 
            else
            {
                //got list of properties
                result = Ok(properties);
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
