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

        public bool CheckIfUserAppliedForThisProperty(int userId, int property_id)
        {
            bool result = false;
            try
            {
                using(SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT userId FROM lease WHERE userId = @userId AND property_id = @property_id", conn);
                    cmd.Parameters.AddWithValue("@userId", userId);
                    cmd.Parameters.AddWithValue("@property_id", property_id);

                    result = (int?)cmd.ExecuteScalar() == userId ? true : false;
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
                throw;     
            }


            return result;
        }

        //get pending leases
        public List<Models.Lease> GetLandlordLeases(int landlord_id)
        {
            List<Models.Lease> list = new List<Models.Lease>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT lease_id, lease.from_date, lease.to_date, lease.userId, lease.property_id, lease.current_status " +
                                                    "FROM lease " +
                                                    "INNER JOIN properties ON properties.property_id = lease.property_id " +
                                                    "INNER JOIN users ON lease.userId = users.userId " +
                                                    "WHERE properties.userId LIKE(SELECT userId FROM users WHERE userId = @landlord_id)", conn);
                    cmd.Parameters.AddWithValue("@landlord_id", landlord_id);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Models.Lease lease = new Models.Lease();
                        lease.Lease_Id = (int)reader["lease_id"];
                        lease.User_Id = (int)reader["userId"];
                        lease.Property_Id = (int)reader["property_id"];
                        lease.From_Date = (DateTime)reader["from_date"];
                        lease.To_Date = (DateTime)reader["to_date"];
                        lease.Lease_Type = (string)reader["current_status"];

                        list.Add(lease);
                    }
                }
            }

            catch (SqlException e)
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

                    if (reader.Read())
                    {
                        lease.Lease_Id = (int)reader["lease_id"];
                        lease.From_Date = (DateTime)reader["from_date"];
                        lease.To_Date = (DateTime)reader["to_date"];
                        lease.User_Id = (int)reader["lease_id"];
                        lease.Property_Id = (int)reader["lease_id"];
                        lease.Lease_Id = (int)reader["lease_id"];
                        lease.Lease_Type = (string)reader["current_status"];
                    } 
                    else
                    {
                        lease = null;
                    }
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

        public int DeletePendingLeaseWithPropertyId(int property_id)
        {
            int rowsAffected = 0;
            try
            {
                using(SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("DELETE FROM lease WHERE property_id = @property_id && current_status='pending'", conn);
                    cmd.Parameters.AddWithValue("@property_id", property_id);


                    rowsAffected = cmd.ExecuteNonQuery();
                }

            }

            catch (SqlException e)
            {
                Console.WriteLine(e);

            }

            return rowsAffected;

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

        public int RejectPendingLeasesWithPropertyId(int property_id)
        {
            int rowCount = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("UPDATE lease SET current_status = 'rejected' WHERE property_id = @property_id AND current_status = 'pending';", conn);
                    cmd.Parameters.AddWithValue("@property_id", property_id);


                    rowCount = cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
            }

            return rowCount;
        }

        public int RejectPendingLeasesWithPropertyId(int property_id, int user_id)
        {
            int rowCount = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("UPDATE lease SET current_status = 'rejected' WHERE (property_id = @property_id XOR user_id = '@user_id');", conn);
                    cmd.Parameters.AddWithValue("@property_id", property_id);
                    cmd.Parameters.AddWithValue("@user_id", user_id);


                    rowCount = cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
            }

            return rowCount;
        }

        public Models.Lease GetAcceptedLeaseWithPropertyId(int property_id)
        {
            Models.Lease lease = new Models.Lease();

            try 
            { 
                using(SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT lease_id, from_date, to_date, userId, property_id " +
                                                    "FROM lease " +
                                                    "WHERE property_id = @property_id " +
                                                    "AND (current_status = 'approved' OR current_status = 'Approved')", conn);

                    cmd.Parameters.AddWithValue("@property_id", property_id);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {

                        lease.Lease_Id = (int)reader["lease_id"];
                        lease.From_Date = (DateTime)reader["from_date"];
                        lease.To_Date = (DateTime)reader["to_date"];
                        lease.User_Id = (int)reader["userId"];
                        lease.Property_Id = (int)reader["property_id"];
                    } 
                    else
                    {
                        lease = null;
                    }
                }
            }
            catch(SqlException e)
            {
                Console.WriteLine(e);
                throw;
            }


            return lease;
        }
    }
}
