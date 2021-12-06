using System;
namespace Core
{
    public interface ITransactionFactory
    {
        public ITransaction Create(double amount, DateTime date);
    }
}

