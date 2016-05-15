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
namespace BudgetFirst.ApplicationCore
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using ViewModel.Repository;

    /// <summary>
    /// Singleton for view model repositories
    /// </summary>
    public static class Repositories
    {
        /// <summary>
        /// Bootstrap which handles all initialisation of the application core
        /// </summary>
        private static readonly Bootstrap Bootstrap;

        /// <summary>
        /// Initialises static members of the <see cref="Repositories"/> class.
        /// </summary>
        static Repositories()
        {
            Bootstrap = new Bootstrap();
        }

        /// <summary>
        /// Gets the account view model repository
        /// </summary>
        public static AccountViewModelRepository AccountViewModelRepository => Bootstrap.AccountViewModelRepository;
    }
}
