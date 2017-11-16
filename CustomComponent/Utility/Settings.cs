using System;
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
