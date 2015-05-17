using System;
using System.Collections.Generic;
using System.Linq;

namespace JS.Library.Settings
{
    /// <summary>
    /// The base class for any settings provider to use.
    /// </summary>
    public abstract class SettingsProvider : ISettingsProvider
    {
        #region Settings Levels
        private List<string> Settings { get; set; }
        private static List<string> GlobalSettings { get; set; }
        #endregion

        #region SystemSettings
        /// <summary>
        /// Gets a system-widesetting. Initializes the system settings if the <paramref name="key"/> is not found.
        /// </summary>
        /// <param name="key">The key of the setting to get.</param>
        /// <returns>The setting as a string.</returns>
        public string GetSystemSetting(string key)
        {
            var setting = GetSystemSettingInner(key);

            if (String.IsNullOrWhiteSpace(setting))
            {
                InitSystemSettings();
                setting = GetSystemSettingInner(key);
            }

            return setting;
        }
        private static string GetSystemSettingInner(string key)
        {
            var setting = String.Empty;
            var settings = ApplicationLogic.Current.SessionProvider.Get<List<string>>(SettingsKeys.SystemSettings);

            if (settings == null)
                return null;

            var item = settings.FirstOrDefault(x => x.StartsWith(key));

            if (item != null)
                setting = item.Split(new[] { "=" }, StringSplitOptions.RemoveEmptyEntries)[1];

            return setting;
        }

        /// <summary>
        /// Sets a system-wide setting. Initializes the settings if null. If the key already exists, it is overwritten.
        /// </summary>
        /// <param name="key">The key of the setting to use.</param>
        /// <param name="value">A string representation of the value to set.</param>
        public void SetSystemSetting(string key, string value)
        {
            var settings = ApplicationLogic.Current.CacheProvider.Get<List<string>>(SettingsKeys.SystemSettings);

            if (settings == null)
            {
                InitSystemSettings();
                settings = Settings;
            }
            else
            {
                var item = settings.FirstOrDefault(x => x.StartsWith(key));

                if (item != null)
                    settings.Remove(item);
            }

            settings.Add(String.Format("{0}={1}", key, value));

            ApplicationLogic.Current.SessionProvider.Set(SettingsKeys.SystemSettings, settings);
        }
        #endregion

        #region User Settings
        /// <summary>
        /// Gets a user-specific setting. Initializes the user settings if the <paramref name="key"/> is not found.
        /// </summary>
        /// <param name="key">The key of the setting to get.</param>
        /// <returns>The setting as a string.</returns>
        public string GetUserSetting(string key)
        {
            var setting = GetUserSettingInner(key);

            if (String.IsNullOrWhiteSpace(setting))
            {
                InitUserSettings();
                setting = GetUserSettingInner(key);
            }

            return setting;
        }
        private static string GetUserSettingInner(string key)
        {
            var setting = String.Empty;
            var settings = ApplicationLogic.Current.SessionProvider.Get<List<string>>(SettingsKeys.UserSettings);

            if (settings == null)
                return null;

            var item = settings.FirstOrDefault(x => x.StartsWith(key));

            if (item != null)
                setting = item.Split(new[] { "=" }, StringSplitOptions.RemoveEmptyEntries)[1];

            return setting;
        }

        /// <summary>
        /// Sets a user-specific setting.
        /// </summary>
        /// <param name="key">The key of the setting to use.</param>
        /// <param name="value">A string representation of the value to set.</param>
        public abstract void SetUserSetting(string key, string value);
        
        #endregion

        #region Private Methods
        private void InitSystemSettings()
        {
            InitGlobalSettings();
            ApplicationLogic.Current.SessionProvider.Set(SettingsKeys.SystemSettings, Settings);
        }
        /// <summary>
        /// Initializes the system-wide settings.
        /// </summary>
        protected abstract void InitGlobalSettings();
        /// <summary>
        /// Initializes the user settings
        /// </summary>
        protected abstract void InitUserSettings();        
        #endregion
    }
}