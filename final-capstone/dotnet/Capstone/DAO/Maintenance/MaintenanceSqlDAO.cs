using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models;


namespace Capstone.DAO.Maintenance
{
    public class MaintenanceSqlDAO : IMaintenanceDAO
    {
        private readonly string connectionString;

        public MaintenanceSqlDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        //get maintenance requests with a list of properties

        //add maintenance ticket to maintenance_request table, return rowsAffected
        public int AddMaintenanceTicket(MaintenanceTicket ticket)
        {

            int rowsAffected = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("INSERT INTO maintenance_request" +
                                                        "(renter_id, request_info, property_id) " +
                                                    "VALUES" +
                                                        "(@Renter_Id, @Request_Info, @Property_Id)", conn);
                    cmd.Parameters.AddWithValue("@Renter_Id", ticket.Renter_Id);
                    cmd.Parameters.AddWithValue("@Request_Info", ticket.Request_Info);
                    cmd.Parameters.AddWithValue("@Property_Id", ticket.Property_Id);

                    rowsAffected = cmd.ExecuteNonQuery();


                }

            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
            }

            return rowsAffected;
            //Renter_Id, UserId, Request_Info, Property_Id
        }

    }
}
