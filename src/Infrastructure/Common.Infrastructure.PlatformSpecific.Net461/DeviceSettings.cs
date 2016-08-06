namespace BudgetFirst.Common.Infrastructure.PlatformSpecific.Net461
{
    using System;

    using BudgetFirst.ApplicationCore.PlatformSpecific;

    /// <summary>
    /// Device settings for .net 4.6.1 platforms (i.e. Windows or Linux)
    /// </summary>
    public class DeviceSettings : IDeviceSettings
    {
        /// <summary>
        /// Settings repository
        /// </summary>
        private SettingsRepository repository = new SettingsRepository();

        /// <summary>
        /// Get the device Id
        /// </summary>
        /// <returns>Device Id</returns>
        public Guid GetDeviceId()
        {
            // TODO: extract the behaviour, it should be the same for every platform:
            // Read persisted device Id - or generate one (and save it) if it doesn't exist yet
            var settings = this.repository.GetSettings();
            if (settings.DeviceId == Guid.Empty)
            {
                settings.DeviceId = Guid.NewGuid();
                settings.Save();
            }

            return settings.DeviceId;
        }
    }
}