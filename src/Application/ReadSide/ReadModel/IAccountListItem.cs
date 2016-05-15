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
namespace BudgetFirst.ReadSide.ReadModel
{
    using System;

    /// <summary>
    /// Read or view model account list item - for use in account lists
    /// </summary>
    public interface IAccountListItem
    {
        /// <summary>
        /// Gets the account Id
        /// </summary>
        Guid Id { get; }

        /// <summary>
        /// Gets the account name
        /// </summary>
        string Name { get; }
    }
}