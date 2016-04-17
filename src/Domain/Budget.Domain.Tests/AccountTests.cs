namespace Budget.Tests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using BudgetFirst.Budget.Domain;
    using BudgetFirst.SharedInterfaces.Messaging;

    [TestClass]
    public class AccountTests
    {
        [TestMethod]
        public void NewAccountHasName()
        {
            var transaction = new EventTransaction();
            var accountFactory = new AccountFactory(transaction);
            var account = accountFactory.CreateAccount("Test1");
            Assert.AreEqual("Test1", account.Name);
        }
    }
}
