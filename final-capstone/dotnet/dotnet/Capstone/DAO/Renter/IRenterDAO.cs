using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.DAO
{
    public interface IRenterDAO
    {
        int AddUserInformation(RenterInformation renter_info);
        BasicRenterInformation GetRenterInformation(int userId);
        string GetRenterAddress(int property_id);
        int GetRenterPropertyIdFromLease(int userID);
        int SetUserRoleToRenter(int userId);
    }
}
