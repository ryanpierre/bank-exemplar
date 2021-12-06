using System;
namespace Core
{
    public interface ITransaction
    {
        public double Amount { get; }
        public DateTime Date { get; }
    }
}
