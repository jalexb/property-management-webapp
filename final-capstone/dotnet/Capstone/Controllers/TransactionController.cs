using Capstone.DAO.Transaction;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models;

namespace Capstone.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TransactionController : Controller
    {

        private readonly ITransactionDAO transactionDAO;
        public TransactionController(ITransactionDAO _transactionDAO)
        {
            transactionDAO = _transactionDAO;
        }

        [HttpGet("{id}")]
        public IActionResult GetTransactionsByRenterId(int id)
        {
            List<Transaction> transactions = new List<Transaction>();
            IActionResult result = BadRequest();

            transactions = transactionDAO.GetTransactionsById(id);

            if(transactions.Count != 0)
            {
                result = Ok(transactions);
            }

            return result;
        }

        [HttpPut("add")]
        public IActionResult leaseAccepted_PopulateTransactions(InitialTransaction initializedTransaction)
        {
            IActionResult result = BadRequest();
            int rowsAffected = 0;

            List<Transaction> transactionList = initializedTransaction.InitializedTransactions();

            foreach (Transaction transaction in transactionList)
            {
                rowsAffected = rowsAffected + transactionDAO.AddTransaction(transaction);
            }
            

            if(rowsAffected == transactionList.Count())
            {
                result = NoContent();
            }

            return result;
        }

    }
}
