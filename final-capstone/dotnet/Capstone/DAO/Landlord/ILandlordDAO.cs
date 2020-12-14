using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.DAO.Landlord
{
    public interface ILandlordDAO
    {
        List<LandlordProperty> GetLandlordProperties(int landlord_id);
        int AddNewPropertyAndAddress(Property property, Address address);
        int UpdatePropertyByPropertyId(Property property);
    }
}
