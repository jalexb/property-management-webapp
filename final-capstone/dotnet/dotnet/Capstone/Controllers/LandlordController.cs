using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.DAO;
using Capstone.DAO.Landlord;
using Capstone.DAO.Transaction;
using Capstone.Models;
using Microsoft.AspNetCore.Mvc;

namespace Capstone.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LandlordController : Controller
    {

        private readonly ILandlordDAO landlordDAO;
        private readonly ILeaseDAO leaseDAO;
        private readonly IRenterDAO renterDAO;
        private readonly ITransactionDAO transactionDAO;
        public LandlordController(ILandlordDAO _landlordDAO, ILeaseDAO _leaseDAO, IRenterDAO _renterDAO, ITransactionDAO _transactionDAO)
        {
            landlordDAO = _landlordDAO;
            leaseDAO = _leaseDAO;
            renterDAO = _renterDAO;
            transactionDAO = _transactionDAO;
        }
        //gets list of rented properties with landlord_id
        //gets list of vacant properties with landlord_id
        //this happend by: getting the list of properties owned by the landlord
        //              getting a list of 'accepted' leases with those property Ids
        //              adding the list of 'accepted' leases to LandlordProperty OccupiedProperties 
        //                  and removing them from the list of properties ownd by the landlord
        //              adding the rest of the list of properties to LandlordProperty VacantProperties

        [HttpGet("/getAllProperties/{landlord_id}")]
        
        public IActionResult GetAllLandlordProperties(int landlord_id)
        {
            //PropertyId, AddressId, Property_Type, Photo, Street, Street2, Price
            List<LandlordProperty> properties = landlordDAO.GetLandlordProperties(landlord_id);
            
            List<LandlordAndPropertyInfoWithTransactionHistory> OccupiedProperties = new List<LandlordAndPropertyInfoWithTransactionHistory>();
            List<LandlordProperty> VacantProperties = new List<LandlordProperty>();
            if (properties.Count() == 0)
            {
                return BadRequest();
            }
        
            foreach (LandlordProperty property in properties)
            {
                Lease lease = GetLeaseFromProperty(property.PropertyId);

                if(lease != null)
                {
                    LandlordAndPropertyInfoWithTransactionHistory propertyAndRenter = new LandlordAndPropertyInfoWithTransactionHistory();
                    propertyAndRenter.Property = property;

                    propertyAndRenter.Renter = populateRenterAndTransactions(lease);

                    if(propertyAndRenter.Renter.Info == null || propertyAndRenter.Renter.TransactionHistory == null)
                    {
                        return BadRequest();
                    }

                    OccupiedProperties.Add(propertyAndRenter);
                } 
                else
                {
                    VacantProperties.Add(property);
                }
            }

            AllLandlordProperties landlordProperties = new AllLandlordProperties();

            landlordProperties.OccupiedProperties = OccupiedProperties;
            landlordProperties.VacantProperties = VacantProperties;

            return Ok(landlordProperties);

        }

        [HttpPost("/addNewProperty")]
        public IActionResult CreateNewPropertyAndAddress(PropertyAndAddress newProperty)
        {
            IActionResult result = BadRequest();

            if (newProperty.Bedrooms < 0 || newProperty.Bathrooms < 0 || newProperty.Price < 0 || newProperty.zip < 0 || newProperty.zip.ToString().Length < 5 || newProperty.zip.ToString().Length > 5)
            {
                return result;
            }

            Property property = new Property();
            Address address = new Address();

            property.userId = newProperty.userId;
            property.Bedrooms = newProperty.Bedrooms;
            property.Bathrooms = newProperty.Bathrooms;
            property.Photo = newProperty.Photo;
            property.Description = newProperty.Description;
            property.Price = newProperty.Price;

            address.User_Id = newProperty.userId;
            address.Property_Type = newProperty.Property_Type;
            address.Street = newProperty.Street;
            if(newProperty.Street2=="")
            {
                address.Street2 = "N/A";
            }
            else
            {
                address.Street2 = newProperty.Street2;
            }
            address.city = newProperty.city;
            address.region = newProperty.region;
            address.zip = newProperty.zip;

            int rowsAffected = landlordDAO.AddNewPropertyAndAddress(property, address);

            if(rowsAffected > 0)
            {
                result = NoContent();
            }

            return result;
        }
        
        [HttpPut("/updateProperty/{propertyId}")]
        public IActionResult UpdatePropertyByPropertyId(int propertyId, Property property)
        {
            IActionResult result = BadRequest();
            int rowsAffected = landlordDAO.UpdatePropertyByPropertyId(property);

            if(rowsAffected > 0)
            {
                result = Ok();
            }

            return result;
        }

        private RenterInformationWithTransactionHistory populateRenterAndTransactions(Lease lease)
        {
            RenterInformationWithTransactionHistory renter = new RenterInformationWithTransactionHistory();
            renter.Info = getRenterInformation(lease.User_Id);
            renter.TransactionHistory = getTransactionHistory(lease.Lease_Id);

            return renter;
        } 

        private BasicRenterInformation getRenterInformation(int renter_id)
        {
            BasicRenterInformation renter = renterDAO.GetRenterInformation(renter_id);

            return renter == null ? null : renter;
        }

        private List<Transaction> getTransactionHistory(int lease_id)
        {
            List<Transaction> transactions = transactionDAO.GetTransactionsByLeaseId(lease_id);

            return transactions.Count == 0 ? null : transactions;
        }

        private Lease GetLeaseFromProperty(int property_id)
        {
            Lease lease = leaseDAO.GetAcceptedLeaseWithPropertyId(property_id);

            return lease != null ? lease : null;
        }
    }
}
