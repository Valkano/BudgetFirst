namespace BudgetFirst.SharedInterfaces.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public interface ICommandBus
    {
        void Submit<TCommand>(TCommand command) where TCommand : ICommand;
    }
}
