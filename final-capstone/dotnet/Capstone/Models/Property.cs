using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{

    public class PropertyAndAddress
    {
        public int propertyId { get; set; }
        public int addressId { get; set; }
        public int userId { get; set; }
        public int Bedrooms { get; set; }
        public decimal Bathrooms { get; set; }
        public string Photo { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Property_Type { get; set; }
        public string Street { get; set; }
        public string Street2 { get; set; }
        public string city { get; set; }
        public string region { get; set; }
        public int zip { get; set; }
    }
    public class Property
    {
        public int propertyId { get; set; }
        public int addressId { get; set; } 
        public int userId { get; set; }
        public int Bedrooms { get; set; }
        public decimal Bathrooms { get; set; }
        public string Photo { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }


    public class Address
    {
        public int User_Id { get; set; } 
        public int Property_Type { get; set; }
        public string Street { get; set; }
        public string Street2 { get; set; }
        public string city { get; set; }
        public string region { get; set; }
        public string zip { get; set; }
    }

}
