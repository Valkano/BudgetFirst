namespace BudgetFirst.Infrastructure.ReadModel
{
    /// <summary>
    /// Represents a read store that can be reset
    /// </summary>
    public interface IResetableReadStore
    {
        /// <summary>
        /// Completely clear the read store.
        /// </summary>
        void Clear();
    }
}