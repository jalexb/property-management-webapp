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

            List<Property> properties = propertyDAO.getProperty();

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
    }
}
