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
        public List<PropertyAndAddress> getProperties()
        {
            List<PropertyAndAddress> properties = new List<PropertyAndAddress>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT property_id, properties.address_id, bedrooms, bathrooms, photo, prop_desc, price, " +
                                                    "property_type, street, street2, city, region, zip " +
                                                    "FROM properties " +
                                                    "INNER JOIN address_table ON properties.address_id = address_table.address_id;", conn);

                    SqlDataReader reader = cmd.ExecuteReader();

                    properties = ConvertToPropertyAndAddress(reader);

                }
            }
            catch(SqlException e)
            {
                Console.WriteLine(e);
            }

            return properties;
        }

        public PropertyAndAddress GetProperty(int id)
        {
            PropertyAndAddress property = new PropertyAndAddress();
            List<PropertyAndAddress> properties = new List<PropertyAndAddress>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT property_id, properties.address_id, bedrooms, bathrooms, photo, prop_desc, price, " +
                                                    "property_type, street, street2, city, region, zip " +
                                                    "FROM properties " +
                                                    "INNER JOIN address_table ON properties.address_id = address_table.address_id " +
                                                    "WHERE property_id = @id", conn);
                    cmd.Parameters.AddWithValue("@id", id);

                    SqlDataReader reader = cmd.ExecuteReader();

                    properties = ConvertToPropertyAndAddress(reader);

                    if(properties.Count == 1)
                    {
                        property = properties[0];
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return property;
        }

        private List<PropertyAndAddress> ConvertToPropertyAndAddress(SqlDataReader reader)
        {
            List<PropertyAndAddress> properties = new List<PropertyAndAddress>();
            while (reader.Read())
            {
                PropertyAndAddress prop = new PropertyAndAddress();

                prop.propertyId = (int)reader["property_id"];
                prop.addressId = (int)reader["address_id"];
                prop.Bedrooms = (int)reader["bedrooms"];
                prop.Bathrooms = (int)reader["bathrooms"];
                prop.Photo = (string)reader["photo"];
                prop.Description = (string)reader["prop_desc"];
                prop.Price = (decimal)reader["price"];
                prop.Property_Type = (string)reader["property_type"];
                prop.Street = (string)reader["street"];
                prop.city = (string)reader["city"];
                prop.region = (string)reader["region"];
                prop.zip = (int)reader["zip"];
                if ((string)reader["street2"] != "N/A")
                {
                    prop.Street2 = (string)reader["street2"];
                }
                else
                {
                    prop.Street2 = "";
                }

                properties.Add(prop);
            }
            return properties;
        }

        public decimal GetPropertyPrice(int property_id)
        {
            decimal price = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT price FROM properties WHERE property_id = @property_id", conn);
                    cmd.Parameters.AddWithValue("@property_id", property_id);

                    price = (decimal)cmd.ExecuteScalar();
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
                throw;
            }

            return price;
        }
    }
}
