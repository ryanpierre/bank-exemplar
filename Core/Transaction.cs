using System;
namespace Core
{
    public class Transaction: ITransaction
    {
        private readonly DateTime _date;
        private readonly double _amount;

        public DateTime Date
        {
            get => _date;
        }

        public double Amount
        {
            get => _amount;
        }

        public Transaction(double amount, DateTime date)
        {
            _amount = amount;
            _date = date;
        }
    }
}
