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
    }
}
