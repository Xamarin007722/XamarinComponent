using System;
using System.IO;
using CustomComponent.Interface;
using SQLite;
using Xamarin.Forms;
using XamarinComponent.iOS.InterfaceImplementation;
using SQLitePCL;

[assembly:Dependency(typeof(SQLiteiOS))]  
namespace XamarinComponent.iOS.InterfaceImplementation
{
    public class SQLiteiOS:ISQLite
    {
        public SQLiteConnection GetConnection ()  
        {  

            var filename = "Settings.db3";
            var folderpath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var filePath = Path.Combine (folderpath, "..", "SettingsFile"); 
            var path = Path.Combine(filePath, filename);
            var connection = new SQLiteConnection(path);
            return connection;
        }

    }
}
