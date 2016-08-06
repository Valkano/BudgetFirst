namespace BudgetFirst.Infrastructure.Persistency
{
    /// <summary>
    /// Factory for persistable application state
    /// </summary>
    public interface ICurrentApplicationStateFactory
    {
        /// <summary>
        /// Get the current application state
        /// </summary>
        /// <returns>Current application state</returns>
        PersistableApplicationState GetCurrentApplicationState();
    }
}