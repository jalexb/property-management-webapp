using Capstone.DAO.Transaction;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Controllers
{
    public class TransactionController : Controller
    {

        private readonly ITransactionDAO transactionDAO;
        public TransactionController(ITransactionDAO _transactionDAO)
        {
            transactionDAO = _transactionDAO;
        }

    }
}
