using System;
using System.Configuration;
using System.Globalization;
using System.Collections.Generic;

namespace EnovaApiService.Configuration
{
    public partial class AppSettings
    {
        public static string EnovaDatabaseName { get; private set; }
        public static string EnovaLogin { get; private set; }
        public static string EnovaPassword { get; private set; }
        public static int DefaultPriceDef { get; private set; }

        public static void Load()
        {
            EnovaDatabaseName = LoadString("EnovaDatabaseName", true);
            EnovaLogin = LoadString("EnovaLogin", true);
            EnovaPassword = LoadString("EnovaPassword", false);
            DefaultPriceDef = LoadInt("DefaultPriceDef", true, "2");
        }

        private static string LoadString(string key, bool required, string defaultValue = null)
        {
            string setting = ConfigurationManager.AppSettings[key] ?? defaultValue;

            if (required && String.IsNullOrEmpty(setting))
            {
                throw new InvalidConfigurationException(key);
            }

            return setting;
        }

        private static int LoadInt(string key, bool required, string defaultValue = null)
        {
            string settingStr = LoadString(key, required, defaultValue);

            if (!Int32.TryParse(settingStr, out int setting))
            {
                throw new InvalidConfigurationException(key);
            }

            return setting;
        }

        private static long LoadLong(string key, bool required, string defaultValue = null)
        {
            string settingStr = LoadString(key, required, defaultValue);

            if (!Int64.TryParse(settingStr, out long setting))
            {
                throw new InvalidConfigurationException(key);
            }

            return setting;
        }

        private static decimal LoadDecimal(string key, bool required, string defaultValue = null)
        {
            string settingStr = LoadString(key, required);

            if (!Decimal.TryParse(settingStr, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out decimal setting))
            {
                throw new InvalidConfigurationException(key);
            }

            return setting;
        }

        private static Guid LoadGuid(string key, bool required, string defaultValue = null)
        {
            string settingsStr = LoadString(key, required);

            if (!Guid.TryParse(settingsStr, out Guid settings))
            {
                throw new InvalidConfigurationException(key);
            }

            return settings;
        }

        private static T LoadEnum<T>(string key, bool required, string defaultValue = null)
            where T : struct
        {
            string settingStr = LoadString(key, required);

            if (!Enum.TryParse(settingStr, out T setting))
            {
                throw new InvalidConfigurationException(key);
            }

            return setting;
        }

    }
}
