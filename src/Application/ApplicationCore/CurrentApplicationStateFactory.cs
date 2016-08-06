namespace BudgetFirst.ApplicationCore
{
    using System;

    using BudgetFirst.Infrastructure.Persistency;

    /// <summary>
    /// Persistable application state factory
    /// </summary>
    public class CurrentApplicationStateFactory : ICurrentApplicationStateFactory
    {
        /// <summary>
        /// Factory delegate
        /// </summary>
        private Func<PersistableApplicationState> factory;

        /// <summary>
        /// Initialises a new instance of the <see cref="CurrentApplicationStateFactory"/> class.
        /// </summary>
        /// <param name="stateFactory">Delegate for the actual state factory</param>
        public CurrentApplicationStateFactory(Func<PersistableApplicationState> stateFactory)
        {
            this.factory = stateFactory;
        }

        /// <summary>
        /// Get the current application state
        /// </summary>
        /// <returns>Current application state</returns>
        public PersistableApplicationState GetCurrentApplicationState()
        {
            return this.factory();
        }
    }
}