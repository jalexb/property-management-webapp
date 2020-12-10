using Capstone.Models;

namespace Capstone.DAO.Renter
{
    public interface IRenterService
    {
        bool SaveRenter(RenterInformationRequest request);
    }
}