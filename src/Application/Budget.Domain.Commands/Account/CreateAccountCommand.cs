namespace BudgetFirst.Budget.Domain.Commands.Account
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using BudgetFirst.SharedInterfaces.Commands;

    public class CreateAccountCommand : ICommand
    {
        public string Name { get; set; }
    }
}
