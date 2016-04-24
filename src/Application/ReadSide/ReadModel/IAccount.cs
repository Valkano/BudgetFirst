namespace BudgetFirst.ReadSide.ReadModel
{
    using System;

    /// <summary>
    /// Account (for view and read models)
    /// </summary>
    public interface IAccount
    {
        /// <summary>
        /// Gets or sets account Id
        /// </summary>
        Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the account name
        /// </summary>
        string Name { get; set; }
    }
}