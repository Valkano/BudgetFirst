namespace BudgetFirst.SharedInterfaces.ApplicationState
{
    using BudgetFirst.SharedInterfaces.Messaging;

    /// <summary>
    /// Provides access to the current vector clock
    /// </summary>
    public interface IVectorClock
    {
        /// <summary>
        /// Increment the current vector clock
        /// </summary>
        void Increment();

        /// <summary>
        /// Create a copy of this vector clock
        /// </summary>
        /// <returns>A clone of this vector clock</returns>
        IVectorClock Clone();

        /// <summary>
        /// Get the current vector clock
        /// </summary>
        /// <returns>The underlying vector clock (as a copy)</returns>
        VectorClock GetVectorClock();

        /// <summary>
        /// Update the vector clock to a new value
        /// </summary>
        /// <param name="value">New value to set</param>
        void Update(IVectorClock value);
    }
}