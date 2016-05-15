namespace BudgetFirst.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Desktop;

    public class ViewModelContainer
    {
        private static ViewModelContainer defaultInstance;

        public static ViewModelContainer Default
        {
            get
            {
                return ViewModelContainer.defaultInstance ??
                    (ViewModelContainer.defaultInstance = new ViewModelContainer());
            }
        }

        private SimpleInjector.Container container;

        public SimpleInjector.Container Container
        {
            get { return this.container; }
        }

        private ViewModelContainer()
        {
            this.container = new SimpleInjector.Container();

            this.container.Register<MainDesktopViewModel>(SimpleInjector.Lifestyle.Singleton);
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
