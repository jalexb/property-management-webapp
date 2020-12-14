using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class Landlord
    {

    }

    //Rented properties, Vacant Properties

    public class AllLandlordProperties
    {
        public List<LandlordPropertyAndRenter> OccupiedProperties { get; set; }
        public List<LandlordProperty> VacantProperties { get; set; }
        public decimal OccupiedRentSum
        {
            get
            {
                decimal rentSum = 0;
                foreach(LandlordPropertyAndRenter property in OccupiedProperties)
                {
                    rentSum += property.Property.Price;
                }


                return rentSum;
            }
        }
        public decimal VacantRentSum
        {
            get
            {
                decimal rentSum = 0;
                foreach (LandlordProperty property in VacantProperties)
                {
                    rentSum += property.Price;
                }


                return rentSum;
            }
        }

        public decimal TotalSum
        {
            get
            {
                return VacantRentSum + OccupiedRentSum;
            }
        }
    }
}
