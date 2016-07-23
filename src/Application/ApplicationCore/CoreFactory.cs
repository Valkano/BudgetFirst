namespace BudgetFirst.ApplicationCore
{
    /// <summary>
    /// Factory for application core
    /// </summary>
    public static class CoreFactory
    {
        /// <summary>
        /// Create a new budget
        /// </summary>
        /// <returns>New application core for new budget</returns>
        public static Core CreateNewBudget()
        {
            return new Core();
        }
    }
}