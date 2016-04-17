using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetFirst.SharedInterfaces.DependencyInjection
{
    public interface IContainer
    {
        TInstance Resolve<TInstance>() where TInstance : class;
        IEnumerable<TInstance> ResolveAll<TInstance>() where TInstance : class;
    }
}
