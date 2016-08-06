namespace BudgetFirst.Common.Infrastructure.PlatformSpecific.Net461
{
    using BudgetFirst.Common.Infrastructure.PlatformSpecific.Net461.Properties;

    /// <summary>
    /// Provides access to the application/user settings
    /// </summary>
    /// <remarks>This is a separate class because this also handles automatic upgrading between versions etc.</remarks>
    internal class SettingsRepository
    {
        /// <summary>
        /// Get the settings. Handles automatic upgrading
        /// </summary>
        /// <returns>Current settings</returns>
        internal Settings GetSettings()
        {
            var settings = Settings.Default;
            if (settings.SettingsRequireUpgrade)
            {
                settings.Upgrade();
                settings.SettingsRequireUpgrade = false;
                settings.Save();
            }

            return settings;
        }
    }
}