namespace BudgetFirst.Budget.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using SharedInterfaces.Domain;
    using SharedInterfaces.Messaging;

    public class Account : AggregateRoot
    {
        internal Account(IEventTransaction eventTransaction, string name) : base(eventTransaction)
        {
            this.Name = name;
        }        

        public string Name { get; private set; }
    }
}
