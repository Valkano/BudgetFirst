namespace BudgetFirst.SharedInterfaces
{
    using System.Dynamic;

    /// <summary>
    /// Contains shared singleton access where dependency injection is not useful
    /// </summary>
    public static class SharedSingletons
    {
        /// <summary>
        /// Gets or sets the current application state.
        /// Setter is for unit tests only. 
        /// </summary>
        public static ApplicationState ApplicationState { get; set; } = new ApplicationState();
    }
}