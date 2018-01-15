using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace UtilityLibrary.Utility
{
    /// <summary>
    /// This library uses the native settings management, which means all settings are persisted across app updates, saved natively, and can be integrated into native settings.
    /// Android: SharedPreferences
    /// Apple: NSUserDefaults
    /// UWP: ApplicationDataContainer
    /// .NET: UserStore -> IsolcatedStorageFile
    ///  Supported data type  -> Boolean,Int32,Int64,String,Single(float),Double,Decimal,DateTime(Stored and retrieved in UTC)
    /// </summary>

    public static class Settings
	{
		private static ISettings AppSettings
		{
			get
			{
				return CrossSettings.Current;
			}
		}

		#region Setting Constants

		private const string SettingsKey = "settings_key";
		private static readonly string SettingsDefault = string.Empty;

		#endregion

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>The name of the user.</value>
		public static string UserName
		{
			get
			{
				return AppSettings.GetValueOrDefault(SettingsKey, SettingsDefault);
			}
			set
			{
				AppSettings.AddOrUpdateValue(SettingsKey, value);
			}
		}

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>The password.</value>
		public static string Password
		{
			get
			{
				return AppSettings.GetValueOrDefault(SettingsKey, SettingsDefault);
			}
			set
			{
				AppSettings.AddOrUpdateValue(SettingsKey, value);
			}
		}
        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>The email.</value>
		public static string Email
		{
			get
			{
				return AppSettings.GetValueOrDefault(SettingsKey, SettingsDefault);
			}
			set
			{
				AppSettings.AddOrUpdateValue(SettingsKey, value);
			}
		}

	}
}
