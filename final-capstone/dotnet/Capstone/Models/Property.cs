using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
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

    public class Lease
    {
        public int Lease_Id { get; set; }
        public DateTime From_Date { get; set; }
        public DateTime To_Date { get; set; }
        public int User_Id { get; set; }
	    public int Property_Id { get; set; }
    }
}
