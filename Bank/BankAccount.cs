// CA3 sample solution - bank account
// author: GC

using System;
using System.Collections;
using System.Text;
using System.Xml.Linq;

namespace Bank
{
    // a bank account
    public class BankAccount : IEnumerable
    {
        // fields
        private String sortCode;
        public String SortCode
        {
            get { return sortCode; }
        }

		private String accountNo;
        
        public String AccountNo
        { get { return accountNo; } }

        public double balance;                 // €
        private double overdraftLimit;          // €  

        private const int MaxTransactions = 100;
        private double[] transactionHistory;   // a record of amounts deposited and withdrawn
        private int nextTransaction;           // the next free slot in the transactionHistory array 

        // constructor
        public BankAccount(String sortCode, String accountNo, double overdraftLimit)
        {
            if (String.IsNullOrWhiteSpace(sortCode))
            {
                throw new ArgumentException("Sort code cannot be empty");
            }
			if (sortCode.Length != 6)
			{
				throw new ArgumentException("Sort code must be 6 digits");
			}
            // int proofInt = int.Parse(sortCode);      // throws System.FormatException
			int intSortCode;
			if (!int.TryParse(sortCode, out intSortCode))
			{
				throw new ArgumentException("Sort code must be an integer");
			}
			else
            {
                this.sortCode = sortCode;
            }

			if (String.IsNullOrWhiteSpace(accountNo))
			{
				throw new ArgumentException("Account Number cannot be empty");
			}
			if (accountNo.Length != 8)
			{
				throw new ArgumentException("Account Number must be 8 digits");
			}
			int intAccountNumber;
            if (!int.TryParse(accountNo, out intAccountNumber))
            {
                throw new ArgumentException("Account Number must be an integer");
            }
            else
            {
                this.accountNo = accountNo;
            }

            this.balance = 0;

            if (overdraftLimit < 0)
            {
                throw new ArgumentException("Overdraft Limit cannot be negative");
            }
            else
            {
                this.overdraftLimit = overdraftLimit;
            }

            transactionHistory = new double[MaxTransactions];
            nextTransaction = 0;                // no transaction to date
        }

        // overloaded constructor - chain
        public BankAccount(String sortCode, String accountNo)
            : this(sortCode, accountNo, 0)
        {
        }


        // deposit money in the account
        public void Deposit(double amount)                      // assume amount is positive
        {
            if (amount < 0)
            {
                throw new ArgumentException("Deposit cannot be less than zero.");
            }
            else
            { 
				balance += amount;
            }

            // record in transaction history
            transactionHistory[nextTransaction++] = amount;
        }

        // withdraw money if there are sufficient funds
        public bool Withdraw(double amount)                     // assume amount is positive
        {
            if ((balance + overdraftLimit) > amount)
            {
                balance -= amount;
                transactionHistory[nextTransaction++] = amount * -1;
                return true;                            // withdraw was succesful
            }
            else                                        // unsufficient funds
            {
                throw new ApplicationException("Insufficient funds!");
            }
        }

        // print account details to the console
        public override string ToString()
        {
            StringBuilder sb1 = new StringBuilder();
			sb1.Append("sort code: " + sortCode + " account no: " + accountNo + " overdraft limit: " + overdraftLimit + " balance: " + balance);
            if (nextTransaction == 0)
            {
                sb1.Append(" | No transaction yet ");
            }
            else
            {
                sb1.Append(" | Transaction History: ");
            }

			for (int i = 0; i < nextTransaction; i++)
			{
				sb1.Append(transactionHistory[i] + " ");
			}
            return sb1.ToString();
		}

        // print the transaction history
        public void PrintTransactionHistory()
        {
            Console.WriteLine("Transaction History:");
            for (int i = 0; i < nextTransaction; i++)
            {
                Console.Write(transactionHistory[i] + " ");
            }
            Console.WriteLine();
        }

		public IEnumerator GetEnumerator()
		{
/*			foreach(Double tx in transactionHistory)
            {
                yield return tx;
            }*/

            for (int i=0; i< nextTransaction; i++)
            {
                yield return transactionHistory[i];
            }
		}
	}
}
