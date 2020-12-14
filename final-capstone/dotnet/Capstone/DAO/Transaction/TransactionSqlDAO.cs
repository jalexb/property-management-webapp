using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models;
using System.Data.SqlClient;

namespace Capstone.DAO.Transaction
{
    public class TransactionSqlDAO : ITransactionDAO
    {
        private readonly string connectionString;

        public TransactionSqlDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public List<Models.Transaction> GetTransactionsById(int id)
        {
            List<Models.Transaction> transactions = new List<Models.Transaction>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT transaction_id,transactions.lease_id, transactions.property_id, payment_due_date, " +
                                                    "late_fees, paid, amount_paid, lease.userId, properties.price " +
                                                    "FROM transactions " +
                                                    "INNER JOIN lease ON lease.lease_id = transactions.lease_id " +
                                                    "INNER JOIN properties ON properties.property_id = lease.property_id " +
                                                    "WHERE lease.userId = @id; ", conn);
                    cmd.Parameters.AddWithValue("@id", id);

                    SqlDataReader reader = cmd.ExecuteReader();

                    transactions = ConvertToTransaction(reader);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return transactions;
        }

        public List<Models.Transaction> GetTransactionsByLeaseId(int lease_id)
        {
            List<Models.Transaction> transactions = new List<Models.Transaction>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT transaction_id,transactions.lease_id, transactions.property_id, payment_due_date, " +
                                                    "late_fees, paid, amount_paid, lease.userId, properties.price " +
                                                    "FROM transactions " +
                                                    "INNER JOIN lease ON lease.lease_id = transactions.lease_id " +
                                                    "INNER JOIN properties ON properties.property_id = lease.property_id " +
                                                    "WHERE lease.lease_id = @lease_id; ", conn);
                    cmd.Parameters.AddWithValue("@lease_id", lease_id);

                    SqlDataReader reader = cmd.ExecuteReader();

                    transactions = ConvertToTransaction(reader);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return transactions;
        }

        public List<Models.Transaction> ConvertToTransaction(SqlDataReader reader)
        {
            List<Models.Transaction> transactionsList = new List<Models.Transaction>();
            
            while (reader.Read())
            {
                Models.Transaction transaction = new Models.Transaction();

                transaction.Transaction_Id = (int)reader["transaction_id"];
                transaction.Lease_Id = (int)reader["lease_id"];
                transaction.Property_Id = (int)reader["property_id"];
                transaction.Payment_Due_Date = (DateTime)reader["payment_due_date"];
                transaction.Late_Fees = (decimal)reader["late_fees"];
                transaction.Paid = (bool)reader["paid"];
                transaction.Amount_Paid = (decimal)reader["amount_paid"];
                transaction.Rent_Price = (decimal)reader["price"];

                transactionsList.Add(transaction);
            }

            return transactionsList;
        }

        public int AddTransaction(Models.Transaction transaction)
        {
            int rowsAffected = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO " +
                                                    "transactions ( lease_id, property_id, payment_due_date, late_fees, paid, amount_paid ) " +
                                                    "VALUES (@Lease_Id, @Property_Id, @Payment_Due_Date, @Late_Fees, @Paid, @Amount_Paid)", conn);
                    cmd.Parameters.AddWithValue("@Lease_Id", transaction.Lease_Id);
                    cmd.Parameters.AddWithValue("@Property_Id", transaction.Property_Id);
                    cmd.Parameters.AddWithValue("@Payment_Due_Date", transaction.Payment_Due_Date);
                    cmd.Parameters.AddWithValue("@Late_Fees", 0);
                    cmd.Parameters.AddWithValue("@Paid", 0);
                    cmd.Parameters.AddWithValue("@Amount_Paid", 0);

                    rowsAffected = cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
            }

            return rowsAffected;
        }

        public int MakePayment(int transactionId, Models.Transaction transaction)
        {
            int rowsAffected = 0;
            SqlCommand cmd;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    if(transaction.Amount_Due == 0)
                    {
                        cmd = new SqlCommand("UPDATE transactions " +
                                             "SET amount_paid = @Amount_Paid, paid = 1 " +
                                             "WHERE transaction_id = @transactionId", conn);
                        
                    }
                    else
                    {
                        cmd = new SqlCommand("UPDATE transactions " +
                                             "SET amount_paid = @Amount_Paid " +
                                             "WHERE transaction_id = @transactionId", conn);
                    }
                    cmd.Parameters.AddWithValue("@Amount_Paid", transaction.Amount_Paid);
                    cmd.Parameters.AddWithValue("@transactionId", transactionId);

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
    }
}
