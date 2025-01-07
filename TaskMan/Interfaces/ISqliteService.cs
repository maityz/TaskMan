using SQLite;

namespace TaskMan.Interfaces;

public interface ISqliteService 
{
	public Task<SQLiteAsyncConnection> GetConnectionAsync();
}