using Bank;

namespace BankTestProject
{
	[TestClass]
	public class UnitTest1
	{
		// Valid Test
		[TestMethod]
		public void TestValidAccount()
		{
			BankAccount b = new BankAccount("903555", "12344544", 1000);	//public BankAccount(String sortCode, String accountNo, double overdraftLimit)
			Assert.IsNotNull(b);
		}

		[TestMethod]
		public void TestDeposit()
		{
			BankAccount b = new BankAccount("903555", "12344544", 1000);
			b.Deposit(150.00);
			Assert.AreEqual(150.00, b.balance);
		}

		// Test Sort Code
		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void TestInvalidSortCode()
		{
			BankAccount b = new BankAccount("", "12344544", 1000);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void TestSortCodeNot6Digits()
		{
			BankAccount b = new BankAccount("90355", "12344544", 1000);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void TestSortCodeNotDigits()
		{
			BankAccount b = new BankAccount("X12345", "12344544", 1000);
		}

		// Test Account Number
		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void TestInvalidAccountNumber()
		{
			BankAccount b = new BankAccount("903555", "", 1000);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void TestAccountNumberNot8Digits()
		{
			BankAccount b = new BankAccount("903555", "1234454", 1000);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void TestAccountNumberNotDigits()
		{
			BankAccount b = new BankAccount("903555", "123445XY", 1000);
		}

		// Test Overdraft Limit
		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void TestOverDraftLimitLessThanZero ()
		{
			BankAccount b = new BankAccount("903555", "12344544", -1000);
		}

		// Test Deposit
		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void TestDepositLessThanZero()
		{
			BankAccount b = new BankAccount("903555", "12344544", 1000);
			b.Deposit(-100);
		}

		// Test Withdrawal
		[TestMethod]
		[ExpectedException(typeof(ApplicationException))]
		public void TestWithdrawGreaterThanBalance()
		{
			BankAccount b = new BankAccount("903555", "12344544", 1000);
			b.Deposit(500);
			b.Withdraw(1600);
		}
	}
}