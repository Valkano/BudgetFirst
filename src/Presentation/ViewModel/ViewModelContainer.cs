// This file is part of BudgetFirst.
//
// BudgetFirst is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// BudgetFirst is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with Budget First.  If not, see<http://www.gnu.org/licenses/>.
// ===================================================================

namespace BudgetFirst.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Desktop;
    using BudgetFirst.Wrappers;

    /// <summary>
    /// A Singleton Container for ViewModels.
    /// </summary>
    public class ViewModelContainer
    {
        /// <summary>
        /// The default instance of the <see cref="ViewModelContainer"/>
        /// </summary>
        private static ViewModelContainer defaultInstance;

        /// <summary>
        /// Prevents a default instance of the <see cref="ViewModelContainer"/> class from being created.
        /// </summary>
        private ViewModelContainer()
        {
            this.Container = new BudgetFirst.Wrappers.Container();

            this.Container.Register<MainDesktopViewModel>(Container.Lifestyle.Singleton);
        }

        /// <summary>
        /// Gets the default instance of the <see cref="ViewModelContainer"/>
        /// </summary>
        public static ViewModelContainer Default => ViewModelContainer.defaultInstance ??
                                                    (ViewModelContainer.defaultInstance = new ViewModelContainer());
        
        /// <summary>
        /// Gets the SimpleInjector Container.
        /// </summary>
        public Container Container { get; private set; }

        /// <summary>
        /// Returns an instance of <see cref="TInstance"/> from the container.
        /// </summary>
        /// <typeparam name="TInstance">The type to return.</typeparam>
        /// <returns>An instantiated <see cref="TInstance"/>.</returns>
        public TInstance Resolve<TInstance>() where TInstance : class
        {
            return this.Container.Resolve<TInstance>();
        }
    }
}
