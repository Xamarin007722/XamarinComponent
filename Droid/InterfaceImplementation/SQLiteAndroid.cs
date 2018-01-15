using System;
using System.IO;
using CustomComponent.Interface;
using SQLite;
using Xamarin.Forms;
using XamarinComponent.Droid.InterfaceImplementation;

[assembly:Dependency(typeof(SQLiteAndroid))]
namespace XamarinComponent.Droid.InterfaceImplementation
{
    public class SQLiteAndroid : ISQLite
    {
        /// <summary>
        /// Creates and Gets the connection.
        /// </summary>
        /// <returns>The connection.</returns>
        public SQLiteConnection GetConnection()
        {
            var filename = "Settings.db3";
            var folderpath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var path = Path.Combine(folderpath, filename);
            var connection = new SQLiteConnection(path);
            return connection;
        }

    }
}
