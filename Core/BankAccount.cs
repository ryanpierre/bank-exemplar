using System;
using System.Collections.Generic;

namespace Core
{
    public class BankAccount
    {
        private readonly ITransactionFactory _transactionFactory;
        private readonly IPrinter _printer;
        private List<ITransaction> _transactions;

        public List<ITransaction> Transactions
        {
            get => _transactions;
        }

        public double Balance
        {
            get {
                double balance = 0;

                _transactions.ForEach(t => {
                    balance += t.Amount;
                });

                return balance;
            }
        }

        public BankAccount() : this(new TransactionFactory(), new Printer()) { }
        public BankAccount(ITransactionFactory transactionFactory) : this(transactionFactory, new Printer()) { }
        public BankAccount(IPrinter printer) : this(new TransactionFactory(), printer) { }
        public BankAccount(List<ITransaction> transactions) : this(new TransactionFactory(), new Printer(), transactions) { }
        public BankAccount(ITransactionFactory transactionFactory, IPrinter printer) : this(transactionFactory, printer, new List<ITransaction>()){ }
        public BankAccount(ITransactionFactory transactionFactory, IPrinter printer, List<ITransaction> transactions)
        {
            _transactionFactory = transactionFactory;
            _printer = printer;
            _transactions = transactions;
        }

        public string GenerateStatement()
        {
            return _printer.PrintStatement(_transactions);
        }

        public void Deposit(double amount, DateTime date)
        {
            _transactions.Add(_transactionFactory.Create(amount, date));
        }

        public void Withdraw(double amount, DateTime date)
        {
            _transactions.Add(_transactionFactory.Create(-1 * amount, date));
        }
    }
}
