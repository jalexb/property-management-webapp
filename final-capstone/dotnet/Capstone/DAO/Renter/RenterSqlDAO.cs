﻿using Capstone.Models;
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
                                                    "(userId, property_id, first_name, last_name, address, " +
                                                    "phone_number, email, lease_type, employment_history, salary, to_date, from_date) " +
                                                    "VALUES(@User_Id, @Property_id, @FirstName, @LastName, @LastName, @Address, @PhoneNumber, " +
                                                    "@Email, @Lease_Type, @Employment_History, @Salary, @To_Date, @From_Date)", conn);

                    cmd.Parameters.AddWithValue("@User_id", renter_info.User_Id);
                    cmd.Parameters.AddWithValue("@FirstName", renter_info.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", renter_info.LastName);
                    cmd.Parameters.AddWithValue("@PhoneNumber", renter_info.PhoneNumber);
                    cmd.Parameters.AddWithValue("@Address", renter_info.Address);
                    cmd.Parameters.AddWithValue("@Email", renter_info.Email);
                    cmd.Parameters.AddWithValue("@Lease_Type", renter_info.Lease_Type);
                    cmd.Parameters.AddWithValue("@Salary", renter_info.Salary);
                    cmd.Parameters.AddWithValue("@Employment_History", renter_info.Employment_History);

                    rowCount = cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
            }

            return rowCount;

        }
    }
}
