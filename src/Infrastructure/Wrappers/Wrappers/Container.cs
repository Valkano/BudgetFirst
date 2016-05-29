using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimpleInjector;
using BudgetFirst.SharedInterfaces.DependencyInjection;

namespace BudgetFirst.Wrappers
{
    public class Container : IContainer
    {
        private SimpleInjector.Container container;

        public Container()
        {
            container = new SimpleInjector.Container();
        }

        public void Register<TService, TImplementation>(Lifestyle lifestyle)
            where TService : class
            where TImplementation : class, TService
        {
            container.Register<TService, TImplementation>(convertLifestyle(lifestyle));
        }

        public void RegisterSingleton<TService>(TService instance) where TService : class
        {
            container.RegisterSingleton<TService>(instance);
        }

        public void Verify()
        {
            this.container.Verify();
        }

        public void Register<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService
        {
            container.Register<TService, TImplementation>();
        }

        public void Register<TConcrete>(Lifestyle lifestyle) where TConcrete : class
        {
            this.container.Register<TConcrete>(convertLifestyle(lifestyle));
        }

        private SimpleInjector.Lifestyle convertLifestyle(Lifestyle lifestyle)
        {
            switch(lifestyle)
            {
                case Lifestyle.Singleton:
                    return SimpleInjector.Lifestyle.Singleton;
                default:
                    throw new NotImplementedException();
            }
        }

        public TInstance Resolve<TInstance>() where TInstance : class
        {
            return this.container.GetInstance<TInstance>();
        }

        public IEnumerable<TInstance> ResolveAll<TInstance>() where TInstance : class
        {
            return this.container.GetAllInstances<TInstance>();
        }

        public enum Lifestyle
        {
            Singleton
        }
    }
}
