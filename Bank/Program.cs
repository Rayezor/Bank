using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank
{
    // test class
    class BankAccountTest
    {
        public static void Main()
        {
            BankAccount b = new BankAccount("903555", "12344544", 1000);
            Console.WriteLine(b.ToString());

            b.Deposit(100);

            if (b.Withdraw(200))
            {
                Console.WriteLine("Withdrawal was successful");
				Console.WriteLine(b.ToString());
			}
            else
            {
                Console.WriteLine("Withdrawal failed");
            }

            b.Deposit(500);
			Console.WriteLine(b.ToString());
			//b.PrintTransactionHistory();
            ;
            Console.WriteLine("Transcations: ");
            foreach(Double tx in b)
            {
                Console.WriteLine(tx);
                Console.WriteLine();
            }
		}
    }
}