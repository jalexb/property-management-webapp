using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.DAO.Transaction
{
    public interface ITransactionDAO
    {
        List<Models.Transaction> GetTransactionsById(int id);
        int AddTransaction(Models.Transaction transaction);
        int MakePayment(int transactionId, Models.Transaction transaction);
    }
}
