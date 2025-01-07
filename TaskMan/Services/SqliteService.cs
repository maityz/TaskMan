using TaskMan.Interfaces;
using TaskMan.Models;
using SQLite;

namespace TaskMan.Services;

public class SqliteService : ISqliteService
{
    private SQLiteAsyncConnection dbConnection;
    public async Task<SQLiteAsyncConnection> GetConnectionAsync()
    {
        if (dbConnection == null)
        {
            dbConnection = new SQLiteAsyncConnection(Path.Combine(FileSystem.AppDataDirectory, "Tasks.db3"));
            await dbConnection.CreateTableAsync<TaskModel>();
            return dbConnection;
        }
        return dbConnection;
    }
}