using System;

namespace Core
{
    class Program
    {
        static void Main(string[] args)
        {
            BankAccount bankAccount = new();

            // Given a client makes a deposit of 1000 on 2021-01-01
            bankAccount.Deposit(1000, DateTime.Parse("Jan 10, 2021"));

            // And a deposit of 2000 on 2021-01-13
            bankAccount.Deposit(2000, DateTime.Parse("Jan 13, 2021"));

            // And a withdrawal of 500 on 2021-01-14
            bankAccount.Withdraw(500, DateTime.Parse("Jan 14, 2021"));

            // When she prints her bank statement
            Console.WriteLine(bankAccount.GenerateStatement());
        }
    }
}
