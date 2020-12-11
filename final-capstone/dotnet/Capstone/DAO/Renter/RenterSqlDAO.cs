using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.DAO
{
    public class RenterSqlDAO : IRenterDAO
    {
        private readonly string connectionString;

        public RenterSqlDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public int AddUserInformation(RenterInformation renter_info)
        {

            int rowCount = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("INSERT INTO renter_information" +
                                                    "(userId, first_name, last_name, " +
                                                    "phone_number, email, salary, lease_type) " +
                                                    "VALUES(@User_Id, @FirstName, @LastName, @PhoneNumber, " +
                                                    "@Email, @Salary, @lease_type)", conn);

                    cmd.Parameters.AddWithValue("@User_id", renter_info.User_Id);
                    cmd.Parameters.AddWithValue("@FirstName", renter_info.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", renter_info.LastName);
                    cmd.Parameters.AddWithValue("@PhoneNumber", renter_info.PhoneNumber);
                    cmd.Parameters.AddWithValue("@Email", renter_info.Email);
                    cmd.Parameters.AddWithValue("@Salary", renter_info.Salary);
                    cmd.Parameters.AddWithValue("@lease_type", "1 year");

                    rowCount = cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
                throw;
            }

            return rowCount;

        }

        public BasicRenterInformation GetRenterInformation(int userId)
        {

            BasicRenterInformation renterInfo = new BasicRenterInformation();

            try
            {
                using(SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT first_name, last_name, phone_number, email FROM renter_information WHERE userId = @userId", conn);
                    cmd.Parameters.AddWithValue("@userId", userId);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while(reader.Read())
                    {
                        renterInfo.FullName = (string)reader["first_name"] + " " + (string)reader["last_name"];
                        renterInfo.PhoneNumber = (string)reader["phone_number"];
                        renterInfo.Email = (string)reader["email"];
                        renterInfo.User_Id = userId;
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
            }

            return renterInfo;

        }

        public BasicRenterInformation GetRenterAddress(BasicRenterInformation renter, int address_id)
        {
            string address = "";
            string street;
            string street2;
            string city;
            string region;
            int zip;
            try
            {
                using(SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT street, street2, city, region, zip FROM address_table WHERE address_id = @address_id", conn);
                    cmd.Parameters.AddWithValue("@address_id", address_id);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while(reader.Read())
                    {
                        street = (string)reader["street"];
                        street2 = (string)reader["street2"];
                        city = (string)reader["city"];
                        region = (string)reader["region"];
                        zip = (int)reader["zip"];

                        address = street + (street2 == "N/A" ? "" : street2) + city + region + zip;
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
            }

            renter.Address = address;

            return renter;
        }

        public int GetRenterPropertyIdFromLease(int userID)
        {
            int propertyId = 0;
            try
            {
                using(SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT property_id FROM lease WHERE userId = @userID", conn);
                    cmd.Parameters.AddWithValue("userID", userID);

                    propertyId = (int)cmd.ExecuteScalar();
                }

            }
            catch(SqlException e)
            {
                Console.WriteLine(e);
            }
            return propertyId;
        }

        

    }
}
