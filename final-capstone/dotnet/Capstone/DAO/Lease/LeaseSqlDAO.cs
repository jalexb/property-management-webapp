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

                    SqlCommand cmd = new SqlCommand("INSERT INTO pending_leases(userId, property_id, from_date, to_date) " +
                                                    "VALUES(@User_Id, @Property_Id, @FromDate, @ToDate)", conn);

                    cmd.Parameters.AddWithValue("@User_Id", lease.User_Id);
                    cmd.Parameters.AddWithValue("@Property_Id", lease.Property_Id);
                    cmd.Parameters.AddWithValue("@FromDate", lease.FromDate);
                    cmd.Parameters.AddWithValue("@ToDate", lease.ToDate);

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
        public List<PendingLease> GetPendingLeases()
        {
            List<PendingLease> list = new List<PendingLease>();


            try
            {
                using(SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT pending_id, userId, property_id, from_date, to_date FROM pending_leases", conn);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while(reader.Read())
                    {
                        PendingLease lease = new PendingLease();

                        lease.Pending_Id = (int)reader["pending_id"];
                        lease.User_Id = (int)reader["userId"];
                        lease.Property_Id = (int)reader["property_id"];
                        lease.FromDate = (DateTime)reader["from_date"];
                        lease.ToDate = (DateTime)reader["to_date"];

                        list.Add(lease);
                    }
                }
            }

            catch(SqlException e)
            {
                Console.WriteLine(e);
            }

            return list;
        }

        public int ApprovePendingLease(Capstone.Models.Lease lease)
        {

            int rowCount = 0;
            try
            {
                using(SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("INSERT INTO lease(from_date, to_date, userId, property_id)" +
                                                    "VALUES(@From_date, @To_Date, @User_Id, @Property_Id)", conn);

                    cmd.Parameters.AddWithValue("@From_date", lease.Fromdate);
                    cmd.Parameters.AddWithValue("@To_Date", lease.Todate);
                    cmd.Parameters.AddWithValue("@User_Id", lease.Userid);
                    cmd.Parameters.AddWithValue("@Property_Id", lease.Propertyid);


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

        public int AddUserInformation(RenterInformation renter_info)
        {

            int rowCount = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("INSERT INTO renter_information" +
                                                    "(userId, first_name, last_name, current_address, " +
                                                    "phone_number, email, lease_type, salary) " +
                                                    "VALUES(@User_Id, @FirstName, @LastName, @Address, @PhoneNumber, " +
                                                    "@Email, @Lease_Type, @Salary)", conn);

                    cmd.Parameters.AddWithValue("@User_id", renter_info.User_Id);
                    cmd.Parameters.AddWithValue("@FirstName", renter_info.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", renter_info.LastName);
                    cmd.Parameters.AddWithValue("@PhoneNumber", renter_info.PhoneNumber);
                    cmd.Parameters.AddWithValue("@Address", renter_info.Address);
                    cmd.Parameters.AddWithValue("@Email", renter_info.Email);
                    cmd.Parameters.AddWithValue("@Lease_Type", renter_info.Lease_Type);
                    cmd.Parameters.AddWithValue("@Salary", renter_info.Salary);

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
