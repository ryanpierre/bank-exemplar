using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Core;
using Moq;
using System.Collections.Generic;

namespace UnitTests
{
    [TestClass]
    public class BankAccountTests
    {
        private Mock<ITransactionFactory> mockTransactionFactory;
        private Mock<ITransaction> mockTransaction;
        private Mock<IPrinter> mockPrinter;

        [TestInitialize]
        public void On_Initialize()
        {
            mockTransactionFactory = new Mock<ITransactionFactory>();
            mockTransaction = new Mock<ITransaction>();
            mockPrinter = new Mock<IPrinter>();

            mockTransaction.Setup(t => t.Amount).Returns(1000);
            mockPrinter.Setup(p => p.PrintStatement(It.IsAny<List<ITransaction>>())).Returns("S_T_A_T_E_M_E_N_T");
            mockTransactionFactory.Setup(tf => tf.Create(It.IsAny<double>(), DateTime.Parse("Jan 10, 2021"))).Returns(mockTransaction.Object);

        }

        [TestMethod]
        public void Can_Deposit()
        {
            BankAccount bankAccount = new(mockTransactionFactory.Object);

            bankAccount.Deposit(1000, DateTime.Parse("Jan 10, 2021"));

            mockTransactionFactory.Verify(tf => tf.Create(1000, DateTime.Parse("Jan 10, 2021")));
            Assert.AreEqual(1, bankAccount.Transactions.Count);
            Assert.AreEqual(mockTransaction.Object, bankAccount.Transactions[0]);
        }

        [TestMethod]
        public void Can_Withdraw()
        {
            BankAccount bankAccount = new(mockTransactionFactory.Object);

            bankAccount.Withdraw(1000, DateTime.Parse("Jan 10, 2021"));

            mockTransactionFactory.Verify(tf => tf.Create(-1000, DateTime.Parse("Jan 10, 2021")));
            Assert.AreEqual(1, bankAccount.Transactions.Count);
            Assert.AreEqual(mockTransaction.Object, bankAccount.Transactions[0]);
        }

        [TestMethod]
        public void Can_Show_Balance()
        {
            List<ITransaction> mockTransactions = new() { mockTransaction.Object, mockTransaction.Object };
            BankAccount bankAccount = new(mockTransactions);

            Assert.AreEqual(2000, bankAccount.Balance);
        }

        [TestMethod]
        public void Can_Generate_Statement()
        {
            BankAccount bankAccount = new(mockPrinter.Object);

            string statement = bankAccount.GenerateStatement();

            mockPrinter.Verify(p => p.PrintStatement(bankAccount.Transactions));

            Assert.AreEqual("S_T_A_T_E_M_E_N_T", statement);
        }
    }
}
