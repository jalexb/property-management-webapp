using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{

    public class PropertyAndAddress
    {
        public int? propertyId { get; set; }
        public int? addressId { get; set; }
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
        public int? propertyId { get; set; }
        public int? addressId { get; set; }
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
        public string Property_Type { get; set; }
        public string Street { get; set; }
        public string Street2 { get; set; }
        public string city { get; set; }
        public string region { get; set; }
        public int zip { get; set; }
    }

    public class LandlordAndPropertyInfoWithTransactionHistory
    {
        public LandlordProperty Property { get; set; }
        public RenterInformationWithTransactionHistory Renter { get; set; }

    } //PropertyId, AddressId, Property_Type, Photo, Street, Street2, Price

    public class LandlordPropertyAndRenterInfo
    {
        public LandlordProperty Property { get; set; }
        public BasicRenterInformation Renter { get; set; }
    }

    public class LandlordProperty
    {
        public int PropertyId { get; set; }
        public int AddressId { get; set; }
        public string Property_Type { get; set; }
        public string Photo { get; set; }
        public string Street { get; set; }
        public string Street2 { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public int Zip { get; set; } //Street Street 2 City Region Zip
        public string Address
        {
            get
            {
                string address = $", {City} {Region}, {Zip}";
                return Street2 == "N/A" ? Street + address : Street + " " + Street2 + address;
            }
        }
        public decimal Price { get; set; }
    }

}
