using CustomComponent.Interface;
using CustomComponent.SearchNearBy.Models;
using SQLite;
using Xamarin.Forms;

namespace CustomComponent.SearchNearBy.Services
{
    /// <summary>
    /// Settings database class to save and get settings page properties value.
    /// </summary>
    public class SettingsDatabase
    {
        static object locker = new object();

        SQLiteConnection settingDatabase;
        /// <summary>
        /// Initializes a new instance of the <see cref="T:CustomComponent.SearchNearBy.Services.SettingsDatabase"/> class.
        /// </summary>
        public  SettingsDatabase()
        {
            settingDatabase = DependencyService.Get<ISQLite>().GetConnection();
            settingDatabase.CreateTable<SettingsDataModel>();
        }

        /// <summary>
        /// Gets the settings page value.
        /// </summary>
        /// <returns>The settings.</returns>
        public SettingsDataModel GetSettings()
        {
            lock (locker)
            {
                if (settingDatabase.Table<SettingsDataModel>().Count() == 0)
                {
                    return null;
                }
                else
                {
                    return settingDatabase.Table<SettingsDataModel>().OrderByDescending(t=>t.ID).First();
                }
            }
        }

        /// <summary>
        /// Saves the settings value.
        /// </summary>
        /// <returns>The settings.</returns>
        /// <param name="model">Model.</param>
        public int SaveSettings(SettingsDataModel model)
        {
            lock (locker)
            {
                return settingDatabase.Insert(model);
            }
        }
    }

}
