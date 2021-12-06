using System;
namespace Core
{
    public class TransactionFactory : ITransactionFactory
    {
        public ITransaction Create(double amount, DateTime date)
        {
            return GetNewTransaction(amount, date);
        }

        protected virtual ITransaction GetNewTransaction(double amount, DateTime date)
        {
            return new Transaction(amount, date);
        }
    }
}