namespace BudgetFirst.Infrastructure.ApplicationState
{
    using System;

    /// <summary>
    /// Contains setters and getters for the current device Id
    /// </summary>
    public interface IDeviceId : IReadOnlyDeviceId
    {
        /// <summary>
        /// Set the current device Id
        /// </summary>
        /// <param name="id">Current device Id</param>
        void SetDeviceId(Guid id);
    }
}