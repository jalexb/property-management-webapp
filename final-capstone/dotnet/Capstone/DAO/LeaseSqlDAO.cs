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

        public bool SaveApplication(Lease lease)
        {
            try
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string statement = @"INSERT INTO [dbo].[pending_leases]
           ([userId]
           ,[property_id]
           ,[from_date]
           ,[to_date]
           ,[first_name]
           ,[last_name]
           ,[phone_num]
           ,[email]
           ,[is_approved])
     VALUES
           ( @user_id
           ,@property_id
           ,@from_date
           ,@to_date
           ,@first_name
           ,@last_name
           ,@phone_num
           ,@email
           ,@is_approved)";
                    var cmd = new SqlCommand(statement, conn);
                    cmd.Parameters.AddWithValue("@user_id", lease.Userid);
                    cmd.Parameters.AddWithValue("@property_id", lease.Propertyid);
                    cmd.Parameters.AddWithValue("@from_date", lease.Fromdate);
                    cmd.Parameters.AddWithValue("@to_date", lease.Todate);
                    cmd.Parameters.AddWithValue("@first_name", lease.Firstname);
                    cmd.Parameters.AddWithValue("@last_name", lease.Lastname);
                    cmd.Parameters.AddWithValue("@phone_num", lease.Phonenumber);
                    cmd.Parameters.AddWithValue("@email", lease.Email);
                    cmd.Parameters.AddWithValue("@is_approved", lease.isapproved);
                    cmd.ExecuteNonQuery();
                }

            }
            catch (Exception ex)
            {

                return false;
            }
            return true;
        }

       /* public List<Pending_Lease>*/
    }
}
