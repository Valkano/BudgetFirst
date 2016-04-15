namespace BudgetFirst.Budget.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Account
    {
        internal Account(string name)
        {
            this.Name = name;
        }        

        public string Name { get; private set; }
    }
}
