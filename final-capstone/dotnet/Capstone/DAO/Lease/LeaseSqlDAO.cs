using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.DAO
{
    public class LeaseSqlDAO : ILeaseDAO
    {
        private readonly string connectionString;

        public LeaseSqlDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }


        

        //add pending lease
        public int AddPendingLease(PendingLease lease)
        {

            int rowCount = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("INSERT INTO lease(userId, property_id, from_date, to_date, current_status) " +
                                                    "VALUES(@User_Id, @Property_Id, @FromDate, @ToDate, @current_status)", conn);

                    cmd.Parameters.AddWithValue("@User_Id", lease.UserId);
                    cmd.Parameters.AddWithValue("@Property_Id", lease.PropertyId);
                    cmd.Parameters.AddWithValue("@FromDate", lease.FromDate);
                    cmd.Parameters.AddWithValue("@ToDate", lease.ToDate);
                    cmd.Parameters.AddWithValue("@current_status", "pending");

                    rowCount = cmd.ExecuteNonQuery();
                }
            }
            catch(SqlException e)
            {
                Console.WriteLine(e);
            }

            return rowCount;
        }


        //get pending leases
        public List<PendingLeaseAndRenterInformation> GetLandlordsPendingLeases(int landlord_id)
        {
            List<PendingLeaseAndRenterInformation> list = new List<PendingLeaseAndRenterInformation>();


            try
            {
                using(SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT lease_id, lease.from_date, lease.to_date, lease.userId, lease.property_id, lease.current_status " +
                                                    "FROM lease INNER JOIN properties ON properties.property_id = lease.property_id " +
                                                    "WHERE properties.userId LIKE(SELECT userId FROM users WHERE userId = @landlord_id) " +
                                                    "AND lease.current_status = 'pending'", conn);
                    cmd.Parameters.AddWithValue("@landlord_id", landlord_id);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while(reader.Read())
                    {
                        Models.Lease lease = new Models.Lease();
                        lease.Lease_Id = (int)reader["lease_id"];
                        lease.User_Id = (int)reader["userId"];
                        lease.Property_Id = (int)reader["property_id"];
                        lease.From_Date = (DateTime)reader["from_date"];
                        lease.To_Date = (DateTime)reader["to_date"];
                        lease.Lease_Type = (string)reader["current_status"];


                        BasicRenterInformation renter_info = GetRenterInformation(lease.User_Id);

                        PendingLeaseAndRenterInformation leaseAndRenter = new PendingLeaseAndRenterInformation();

                        leaseAndRenter.Pending_Lease = lease;
                        leaseAndRenter.Renter_Info = renter_info;

                        list.Add(leaseAndRenter);
                    }
                }
            }

            catch(SqlException e)
            {
                Console.WriteLine(e);
            }

            return list;
        }

        public Models.Lease GetLease(int lease_id)
        {
            Models.Lease lease = new Models.Lease();

            try
            {
                using(SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT lease_id, from_date, to_date, userId, property_id, current_status FROM lease WHERE lease_id = @lease_id", conn);
                    cmd.Parameters.AddWithValue("@lease_id", lease_id);

                    SqlDataReader reader = cmd.ExecuteReader();

                    lease.Lease_Id = (int)reader["lease_id"];
                    lease.From_Date = (DateTime)reader["from_date"];
                    lease.To_Date = (DateTime)reader["lease_id"];
                    lease.User_Id = (int)reader["lease_id"];
                    lease.Property_Id = (int)reader["lease_id"];
                    lease.Lease_Id = (int)reader["lease_id"];
                    lease.Lease_Type = (string)reader["current_status"];
                }
            }
            catch(SqlException e)
            {
                Console.WriteLine(e);
            }

            return lease;
        }

        public int ApprovePendingLease(int lease_id)
        {

            int rowCount = 0;
            try
            {
                using(SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("UPDATE lease SET current_status = 'approved' WHERE lease_id = @lease_id;", conn);
                    cmd.Parameters.AddWithValue("@lease_id", lease_id);


                    rowCount = cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
            }

            return rowCount;
        }

        public int DeletePendingLease(int pending_id)
        {
            int rowsAffected = 0;
            try
            {
                using(SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("DELETE FROM pending_leases WHERE pending_id = @pending_id", conn);
                    cmd.Parameters.AddWithValue("@pending_id", pending_id);


                    rowsAffected = cmd.ExecuteNonQuery();
                }

            }

            catch (SqlException e)
            {
                Console.WriteLine(e);

            }

            return rowsAffected;

        }

        public BasicRenterInformation GetRenterInformation(int userId)
        {

            BasicRenterInformation renterInfo = new BasicRenterInformation();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT first_name, last_name, phone_number, email FROM renter_information WHERE userId = @userId", conn);
                    cmd.Parameters.AddWithValue("@userId", userId);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
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

        public int RejectPendingLease(int lease_id)
        {
            int rowCount = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("UPDATE lease SET current_status = 'rejected' WHERE lease_id = @lease_id;", conn);
                    cmd.Parameters.AddWithValue("@lease_id", lease_id);


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
