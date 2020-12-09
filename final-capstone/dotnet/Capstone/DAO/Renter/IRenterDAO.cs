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
    }
}
