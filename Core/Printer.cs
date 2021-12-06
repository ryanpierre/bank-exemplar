using System;
using System.Collections.Generic;
using System.Globalization;

namespace Core
{
    public class Printer : IPrinter
    {
        private string _defaultHeader = "date || credit || debit || balance";

        public string PrintStatement(List<ITransaction> transactions)
        {
            return PrintHeader() + AggregateRows(transactions);
        }

        private string PrintHeader()
        {
            return _defaultHeader;
        }

        private string AggregateRows(List<ITransaction> transactions)
        {
            string output = "";
            double balance = 0;

            transactions.ForEach(t => {
                balance += t.Amount;
                output += "\n" + PrintTransaction(t, balance);
             });

            return output;
        }

        private string PrintTransaction(ITransaction transaction, double currentBalance)
        {
            string date = transaction.Date.ToString("dd/MM/yyyy");
            string credit = transaction.Amount >= 0 ? transaction.Amount.ToString("#.00", CultureInfo.InvariantCulture) : "-";
            string debit = transaction.Amount < 0 ? Math.Abs(transaction.Amount).ToString("#.00", CultureInfo.InvariantCulture) : "-";
            string balance = currentBalance.ToString("#.00", CultureInfo.InvariantCulture);
            return date + " || " + credit + " || " + debit + " || " + balance;
        }
    }
}
