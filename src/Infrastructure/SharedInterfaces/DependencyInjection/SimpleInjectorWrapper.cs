using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetFirst.SharedInterfaces.DependencyInjection
{
    public class SimpleInjectorWrapper : IContainer
    {
        private SimpleInjector.Container container;

        public SimpleInjectorWrapper(SimpleInjector.Container container)
        {
            this.container = container;
        }

        public TInstance Resolve<TInstance>() where TInstance : class
        {
            return this.container.GetInstance<TInstance>();
        }

        public IEnumerable<TInstance> ResolveAll<TInstance>() where TInstance : class
        {
            return this.container.GetAllInstances<TInstance>();
        }
    }
}
