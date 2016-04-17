namespace BudgetFirst.Budget.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using SharedInterfaces.Messaging;

    public class AccountFactory
    {
        private IEventTransaction eventTransaction;

        public AccountFactory(IEventTransaction eventTransaction)
        {
            this.eventTransaction = eventTransaction;
        }

        private IEventTransaction GetTransaction()
        {
            return this.eventTransaction;
        }

        public Account CreateAccount(string name)
        {
            return new Account(GetTransaction(), name);
        }
    }
}
