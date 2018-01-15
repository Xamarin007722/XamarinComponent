using CustomComponent.Interface;
using CustomComponent.SearchNearBy.Models;
using SQLite;
using Xamarin.Forms;

namespace CustomComponent.SearchNearBy.Services
{
    /// <summary>
    /// Settings token controller.
    /// </summary>
    public class SettingsTokenController
    {
        static object locker = new object();

        SQLiteConnection settingDatabase;
        /// <summary>
        /// Manages the connection.
        /// </summary>
        public void ManageConnection()
        {
            settingDatabase = DependencyService.Get<ISQLite>().GetConnection();
            settingDatabase.CreateTable<TokenModel>();
        }
        /// <summary>
        /// Gets the settings.
        /// </summary>
        /// <returns>The settings.</returns>
        public TokenModel GetSettings()
        {
            lock (locker)
            {
                if (settingDatabase.Table<TokenModel>().Count() == 0)
                {
                    return null;
                }
                else
                {
                    return settingDatabase.Table<TokenModel>().First();
                }
            }
        }
        /// <summary>
        /// Saves the settings.
        /// </summary>
        /// <returns>The settings.</returns>
        /// <param name="model">Model.</param>
        public int SaveSettings(TokenModel model)
        {
            lock (locker)
            {
                if (model.ID != 0)
                {
                    settingDatabase.Update(model);
                    return model.ID;
                }
                else
                {
                    return settingDatabase.Insert(model);
                }
            }
        }

        public SettingsTokenController()
        {
        }
    }
}
