namespace BudgetFirst.CommandBus
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using SharedInterfaces;
    using SharedInterfaces.Commands;

    public class CommandBus : ICommandBus
    {
        public void Submit<TCommand>(TCommand command) where TCommand : ICommand
        {
            throw new NotImplementedException();
        }
    }
}
