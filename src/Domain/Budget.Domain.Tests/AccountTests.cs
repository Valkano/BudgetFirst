namespace Budget.Tests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using BudgetFirst.Budget.Domain;

    [TestClass]
    public class AccountTests
    {
        [TestMethod]
        public void NewAccountHasName()
        {
            var account = AccountFactory.CreateAccount("Test1");
            Assert.AreEqual("Test1", account.Name);
        }
    }
}
