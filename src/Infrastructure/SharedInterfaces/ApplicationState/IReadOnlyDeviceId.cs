namespace BudgetFirst.SharedInterfaces.ApplicationState
{
    using System;

    /// <summary>
    /// Contains the current device Id
    /// </summary>
    public interface IReadOnlyDeviceId
    {
        /// <summary>
        /// Get the current device id
        /// </summary>
        /// <returns>Current device Id</returns>
        Guid GetDeviceId();
    }
}