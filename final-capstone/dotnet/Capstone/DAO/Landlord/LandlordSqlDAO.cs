using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models;


namespace Capstone.DAO.Landlord
{
    public class LandlordSqlDAO : ILandlordDAO
    {
        private readonly string connectionString;

        public LandlordSqlDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }


        //gets list of rented properties with landlord_id
        //gets list of vacant properties with landlord_id
        //this happend by: getting the list of properties owned by the landlord
        //              getting a list of 'accepted' leases with those property Ids
        //              adding the list of 'accepted' leases to LandlordProperty OccupiedProperties 
        //                  and removing them from the list of properties ownd by the landlord
        //              adding the rest of the list of properties to LandlordProperty VacantProperties


        //gets all landlord's properties
        public List<LandlordProperty> GetLandlordProperties(int landlord_id)
        {
            List<LandlordProperty> properties = new List<LandlordProperty>();

            try
            {
                using(SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT property_id, address_id, photo, price FROM properties WHERE userId = @landlord_id", conn);
                    cmd.Parameters.AddWithValue("@landlord_id", landlord_id);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while(reader.Read())
                    {
                        LandlordProperty property = new LandlordProperty();

                        property.PropertyId = (int)reader["property_id"];
                        property.AddressId = (int)reader["address_id"];
                        property.Photo = (string)reader["photo"];
                        property.Price = (decimal)reader["price"];

                        properties.Add(property);
                        
                        //PropertyId, AddressId, Property_Type, Photo, Street, Street2, Price
                    }

                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
                throw;
            }

            return properties;

        }
    }
}
