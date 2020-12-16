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

        public List<TicketAndAddress> GetAssignedTicketsByUserId(int userId)
        {
            List<TicketAndAddress> tickets = new List<TicketAndAddress>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT request_id, renter_id, worker_id, request_info, maintenance_request.property_id, " +
                                                    "is_assigned, is_fixed, post_fix_report, address_table.street, address_table.street2 " +
                                                    "FROM maintenance_request " +
                                                    "INNER JOIN properties ON properties.property_id = maintenance_request.property_id " +
                                                    "INNER JOIN address_table ON address_table.address_id = properties.address_id " +
                                                    "WHERE worker_id = @worker_id AND is_assigned = 1", conn);
                    cmd.Parameters.AddWithValue("@worker_id", userId);

                    SqlDataReader reader = cmd.ExecuteReader();

                    tickets = ConvertToTicketAndAddress(reader);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return tickets;
        }

        public int MarkTicketCompleted(int request_Id, TicketAndAddress ticket)
        {
            int rowsAffected = 0;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("UPDATE maintenance_request " +
                                                    "SET is_fixed = 1, post_fix_report = @report " +
                                                    "WHERE request_id = @request_id", conn);
                    cmd.Parameters.AddWithValue("@report", ticket.Post_Fix_Report);
                    cmd.Parameters.AddWithValue("@request_id", request_Id);

                    rowsAffected = cmd.ExecuteNonQuery();
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return rowsAffected;
        }

        private List<TicketAndAddress> ConvertToTicketAndAddress(SqlDataReader reader)
        {
            List<TicketAndAddress> tickets = new List<TicketAndAddress>();

            while(reader.Read())
            {
                TicketAndAddress ticket = new TicketAndAddress();

                ticket.Request_Id = (int)reader["request_id"];
                ticket.Renter_Id = (int)reader["renter_id"];
                ticket.Worker_Id = (int)reader["worker_id"];
                ticket.Request_Info = (string)reader["request_info"];
                ticket.Property_Id = (int)reader["property_id"];
                ticket.Is_Assigned = (bool)reader["is_assigned"];
                ticket.Is_Fixed = (bool)reader["is_fixed"];
                ticket.Post_Fix_Report = (string)reader["post_fix_report"];
                ticket.Street = (string)reader["street"];
                ticket.Street2 = (string)reader["street2"];

                tickets.Add(ticket);
            }

            return tickets;
        }
    }
}
