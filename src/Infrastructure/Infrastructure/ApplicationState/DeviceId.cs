namespace BudgetFirst.Infrastructure.ApplicationState
{
    using System;

    /// <summary>
    /// Contains the current device Id
    /// </summary>
    public class DeviceId : IDeviceId
    {
        /// <summary>
        /// Current device Id
        /// </summary>
        private Guid deviceId;

        /// <summary>
        /// Set the current device Id
        /// </summary>
        /// <param name="id">Current device Id</param>
        public void SetDeviceId(Guid id)
        {
            this.deviceId = id;
        }

        /// <summary>
        /// Get the current device Id
        /// </summary>
        /// <returns>The current device Id</returns>
        public Guid GetDeviceId()
        {
            return this.deviceId;
        }

        /// <summary>
        /// To string
        /// </summary>
        /// <returns>String value of device id. Always formatted the same</returns>
        public override string ToString()
        {
            return this.deviceId.ToString("d");
        }
    }
}