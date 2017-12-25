using System;
using CustomComponent.Interface;
using CustomComponent.SearchNearBy.Models;
using SQLite;
using Xamarin.Forms;

namespace CustomComponent.SearchNearBy.Services
{
    public class SettingsDatabase
    {
        static object locker = new object();

        SQLiteConnection settingDatabase;
        public  SettingsDatabase()
        {
            settingDatabase = DependencyService.Get<ISQLite>().GetConnection();
            settingDatabase.CreateTable<SettingsDataModel>();
        }

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

        public int SaveSettings(SettingsDataModel model)
        {
            lock (locker)
            {
                return settingDatabase.Insert(model);
            }
        }
    }

}
