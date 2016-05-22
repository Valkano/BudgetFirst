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
    using BudgetFirst.SharedInterfaces.Commands;
    using BudgetFirst.SharedInterfaces.Messaging;

    /// <summary>
    /// Represents the Application's core functionality as a Singleton.
    /// </summary>
    public class Core
    {
        /// <summary>
        /// The backing variable for the Singleton instance of <see cref="Core"/>.
        /// </summary>
        private static Core defaultInstance;

        /// <summary>
        /// The Core's Bootstrap.
        /// </summary>
        private readonly Bootstrap bootstrap = new Bootstrap();

        /// <summary>
        /// Prevents a default instance of the <see cref="Core"/> class from being created.
        /// </summary>
        private Core()
        {
            this.Repositories = new Repositories(this.bootstrap);
            this.MessageBus = this.bootstrap.MessageBus;
            this.CommandBus = this.bootstrap.CommandBus;
        }

        /// <summary>
        /// The singleton instance of <see cref="Core"/>
        /// </summary>
        public static Core Default => Core.defaultInstance ?? (Core.defaultInstance = new Core());

        /// <summary>
        /// Gets the Application's Repositories.
        /// </summary>
        public Repositories Repositories { get; private set; }

        /// <summary>
        /// Gets the  Application's MessageBus.
        /// </summary>
        public IMessageBus MessageBus { get; private set; }

        /// <summary>
        /// Gets the  Application's CommandBus
        /// </summary>
        public ICommandBus CommandBus { get; private set; }
    }
}
