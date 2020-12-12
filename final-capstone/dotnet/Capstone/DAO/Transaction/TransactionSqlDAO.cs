using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.DAO.Transaction
{
    public class TransactionSqlDAO : ITransactionDAO
    {
        private readonly string connectionString;

        public TransactionSqlDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }
    }
}
