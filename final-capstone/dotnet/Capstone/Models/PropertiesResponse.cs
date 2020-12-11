using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class PropertiesResponse
    {
        public int PropertyId { get; set; }
        public int LandlordId { get; set; }
        public int AddressId { get; set; }
        public int Bedrooms { get; set; }
        public int Bathrooms { get; set; }
        public string Photo { get; set; }
        public string PropDesc { get; set; }
        public decimal Price { get; set; }
    }
}
