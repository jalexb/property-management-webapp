using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Capstone.Models;

namespace Capstone.DAO
{
    public class PropertySqlDAO : IPropertyDAO
    {
        private readonly string connectionString;

        public PropertySqlDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }


        /// <summary>
        /// Gets all properties from the properties table and returns all properties
        /// </summary>
        public List<Property> getProperty()
        {
            List<Property> property = new List<Property>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT property_id, address_id, userId, bedrooms, bathrooms, photo, prop_desc, price " +
                                                    "FROM properties", conn);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while(reader.Read())
                    {
                        Property prop = new Property();

                        prop.propertyId = (int)reader["property_id"];
                        prop.addressId = (int)reader["address_id"];
                        prop.userId = (int)reader["userId"];
                        prop.Bedrooms = (int)reader["bedrooms"];
                        prop.Bathrooms = (int)reader["bathrooms"];
                        prop.Photo = (string)reader["photo"];
                        prop.Description = (string)reader["prop_desc"];
                        prop.Price = (decimal)reader["price"];


                        property.Add(prop);
                    }

                }
            }
            catch(SqlException e)
            {
                Console.WriteLine(e);
            }



            return property;
        }


    }
}
