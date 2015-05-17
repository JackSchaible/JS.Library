namespace JS.Library.Settings
{
    /// <summary>
    /// Provides a method for storing user-specific or system-wide settings.
    /// </summary>
    public interface ISettingsProvider
    {
        /// <summary>
        /// Gets a user-specific setting.
        /// </summary>
        /// <param name="key">The key of the setting to get.</param>
        /// <returns>The setting as a string.</returns>
        string GetUserSetting(string key);

        /// <summary>
        /// Sets a user-specific setting.
        /// </summary>
        /// <param name="key">The key of the setting to use.</param>
        /// <param name="value">A string representation of the value to set.</param>
        void SetUserSetting(string key, string value);

        /// <summary>
        /// Gets a system-widesetting.
        /// </summary>
        /// <param name="key">The key of the setting to get.</param>
        /// <returns>The setting as a string.</returns>
        string GetSystemSetting(string key);

        /// <summary>
        /// Sets a system-wide setting.
        /// </summary>
        /// <param name="key">The key of the setting to use.</param>
        /// <param name="value">A string representation of the value to set.</param>
        void SetSystemSetting(string key, string value);
    }
}
