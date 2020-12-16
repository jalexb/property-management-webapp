using Capstone.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class Transaction
    {
        public int Transaction_Id { get; set; }
        public int Lease_Id { get; set; } 
        public int Property_Id { get; set; } 
        public DateTime Payment_Due_Date { get; set; } 
        public decimal Late_Fees { get; set; } 
        public bool Paid { get; set; }
        public decimal Amount_Paid { get; set; }
        public decimal Rent_Price { get; set; }
        public decimal Amount_Due { get { return (Rent_Price + Late_Fees) - Amount_Paid; } }

        //Transaction_Id, Lease_Id, Property_Id, Payment_Due_Date, Late_Fees, Paid, Amount_Paid, Rent_Price
    }

    public class InitialTransaction
    {
        public int Lease_Id { get; set; }
        public int Property_Id { get; set; }
        public DateTime From_Date { get; set; }
        public DateTime To_Date { get; set; }
        public decimal Rent_Price { get; set; } //Lease_Id, Property_Id, From_Date, To_Date, Rent_Price
        //Lease_Id, Property_Id, From_Date, To_Date


        public List<Transaction> InitializedTransactions()
        {
            List<Transaction> transactions = new List<Transaction>();

            while(From_Date < To_Date)
            {//Transaction_Id, Lease_Id, Property_Id, Payment_Due_Date, Late_Fees, Paid, Amount_Paid, Rent_Price
                Transaction transaction = new Transaction();

                transaction.Lease_Id = Lease_Id;
                transaction.Property_Id = Property_Id;
                transaction.Payment_Due_Date = From_Date;
                transaction.Late_Fees = 0;
                transaction.Paid = false;
                transaction.Amount_Paid = 0;
                transaction.Rent_Price = Rent_Price;
                From_Date = From_Date.AddMonths(1);
                


                transactions.Add(transaction);
            }

            return transactions;
        }
    }
}
