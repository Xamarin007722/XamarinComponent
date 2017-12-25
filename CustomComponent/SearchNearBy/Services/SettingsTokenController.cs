using System;
using CustomComponent.Interface;
using CustomComponent.SearchNearBy.Models;
using SQLite;
using Xamarin.Forms;

namespace CustomComponent.SearchNearBy.Services
{
    public class SettingsTokenController
    {
        static object locker = new object();

        SQLiteConnection settingDatabase;
        public void ManageConnection()
        {
            settingDatabase = DependencyService.Get<ISQLite>().GetConnection();
            settingDatabase.CreateTable<TokenModel>();
        }

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
