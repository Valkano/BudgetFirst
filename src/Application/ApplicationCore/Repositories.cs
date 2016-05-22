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
    using BudgetFirst.ReadSide.Repositories;

    /// <summary>
    /// A class that holds references to the Application's Read Model Repositories.
    /// </summary>
    public class Repositories
    {
        /// <summary>
        /// bootstrap which handles all initialisation of the application core
        /// </summary>
        private readonly Bootstrap bootstrap;

        /// <summary>
        /// Initialises a new instance of the <see cref="Repositories"/> class.
        /// </summary>
        /// <param name="bootstrap">The application core's bootstrap</param>
        internal Repositories(Bootstrap bootstrap)
        {
            this.bootstrap = bootstrap;
        }

        /// <summary>
        /// Gets the account read model repository
        /// </summary>
        public AccountReadModelRepository AccountReadModelRepository => this.bootstrap.AccountReadModelRepository;

        /// <summary>
        /// Gets the Account List read model Repository
        /// </summary>
        public AccountListReadModelRepository AccountListReadModelRepository => this.bootstrap.AccountListReadModelRepository;
    }
}
