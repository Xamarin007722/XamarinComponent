using System;
using SQLite;

namespace CustomComponent.Interface
{
    public interface ISQLite
    {
        SQLiteConnection GetConnection();
    }
}
