namespace BudgetFirst.Budget.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class AccountFactory
    {
        public static Account CreateAccount(string name)
        {
            return new Account(name);
        }
    }
}
