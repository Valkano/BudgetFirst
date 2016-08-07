namespace BudgetFirst.Events
{
    using System;

    /// <summary>
    /// Registry for known types for serialisation support.
    /// Contains all known types needed for serialisation or de-serialisation
    /// </summary>
    /// <remarks>Usually we would determine the known types at runtime via reflection, but we cannot use reflection in a PCL.</remarks>
    public static class KnownTypesRegistry
    {
        /// <summary>
        /// Initialises static members of the <see cref="KnownTypesRegistry"/> class.
        /// </summary>
        static KnownTypesRegistry()
        {
            EventTypes = new[]
            {
                typeof(Events.AccountCreated),
                typeof(Events.AccountNameChanged),
            };
        }

        /// <summary>
        /// Gets all known types in this assembly
        /// </summary>
        public static Type[] EventTypes { get; }
    }
}