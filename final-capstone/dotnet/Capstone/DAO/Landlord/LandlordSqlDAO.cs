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

                    SqlCommand cmd = new SqlCommand("SELECT property_id, properties.address_id, property_type, photo, street, street2, price, zip, region, city " +
                                                    "FROM properties " +
                                                    "INNER JOIN address_table on address_table.address_id = properties.address_id " +
                                                    "WHERE properties.userId = @landlord_id", conn);

                    cmd.Parameters.AddWithValue("@landlord_id", landlord_id);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while(reader.Read())
                    {
                        LandlordProperty property = new LandlordProperty();

                        property.PropertyId = (int)reader["property_id"];
                        property.AddressId = (int)reader["address_id"];
                        property.Photo = (string)reader["photo"];
                        property.Price = (decimal)reader["price"];
                        property.Street = (string)reader["street"];
                        property.Street2 = (string)reader["street2"];
                        property.Zip = (int)reader["zip"];
                        property.Region = (string)reader["region"];
                        property.City = (string)reader["city"];
                        property.Property_Type = (string)reader["property_type"];

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

        public int AddNewPropertyAndAddress(Property property, Address address)
        {
            int rowsAffected = 0;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand addressCmd = new SqlCommand("INSERT INTO address_table (userId, property_type, street, street2, city, region, zip) " +
                                                           "VALUES (@userId, @property_type, @street, @street2, @city, @region, @zip)", conn);
                    addressCmd.Parameters.AddWithValue("@userId", address.User_Id);
                    addressCmd.Parameters.AddWithValue("@property_type", address.Property_Type);
                    addressCmd.Parameters.AddWithValue("@street", address.Street);
                    addressCmd.Parameters.AddWithValue("@street2", address.Street2);
                    addressCmd.Parameters.AddWithValue("@city", address.city);
                    addressCmd.Parameters.AddWithValue("@region", address.region);
                    addressCmd.Parameters.AddWithValue("@zip", address.zip);

                    rowsAffected = addressCmd.ExecuteNonQuery();
                    
                    if(rowsAffected > 0)
                    {
                        SqlCommand propertyCmd = new SqlCommand("INSERT INTO properties (userId, address_id, bedrooms, bathrooms, photo, prop_desc, price) " +
                                                            "VALUES (@userId, (SELECT MAX(address_id) FROM address_table), @bedrooms, @bathrooms, @photo, @prop_desc, @price)", conn);
                        propertyCmd.Parameters.AddWithValue("@userId", property.userId);
                        propertyCmd.Parameters.AddWithValue("@bedrooms", property.Bedrooms);
                        propertyCmd.Parameters.AddWithValue("@bathrooms", property.Bathrooms);
                        propertyCmd.Parameters.AddWithValue("@photo", property.Photo);
                        propertyCmd.Parameters.AddWithValue("@prop_desc", property.Description);
                        propertyCmd.Parameters.AddWithValue("@price", property.Price);

                        rowsAffected += propertyCmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return rowsAffected;
        }
    }
}
