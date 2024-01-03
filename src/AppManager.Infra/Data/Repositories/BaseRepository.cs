using Dapper;
using Microsoft.EntityFrameworkCore;

using AppManager.Domain.Entities;
using AppManager.Domain.Interfaces.Repositories;
using AppManager.Infra.Data.Context;
using AppManager.Infra.Data.Mapping;

namespace AppManager.Infra.Data.Repositories;

public abstract class BaseRepository<T> : 
	IBaseRepository<T> where T : BaseEntity
{
	// Read
	protected readonly DapperDbContext _dapperConnect;
	// Write
	protected EfDbContext _efContext;
	protected DbSet<T> _datasetEfContext;
	protected readonly string _tableNameWithSchema;

	public BaseRepository(
		EfDbContext context,
		DapperDbContext readContext,
		SchemaNames schemaName,
		TableNames tableName
	)
	{
		_efContext = context;
		_datasetEfContext = _efContext.Set<T>();

		_dapperConnect = readContext;
		_tableNameWithSchema = $"{schemaName.Value}.{tableName.Value}";
	}

	#region ReadContext

	public string GetTableNameWithSchema() => _tableNameWithSchema;

	public async Task<int> Count()
	{		
		var result = await _dapperConnect.Connection.ExecuteScalarAsync<int>($"SELECT COUNT(*) FROM {_tableNameWithSchema}");
		_dapperConnect.Close();
		return result;
	}

	public async Task<bool> ExistAsync(Guid id)
	{
		var query = $"SELECT Id FROM {_tableNameWithSchema} WHERE Id = @ItemId";
		var result = await _dapperConnect.Connection.QueryFirstOrDefaultAsync<T>(query, new { ItemId = id });
		_dapperConnect.Connection.Close();
		return result is not null;
	}
	public async Task<T?> GetByIdAsync(Guid id)
	{
		var query = $"SELECT * FROM {_tableNameWithSchema} WHERE Id = @ItemId";
		var result = await _dapperConnect.Connection.QueryFirstOrDefaultAsync<T>(query, new { ItemId = id });
		_dapperConnect.Connection.Close();
		return result;
	}

	public async Task<IEnumerable<T>> GetAsync(string command, object? parameters)
	{
		var result = await _dapperConnect.Connection.QueryAsync<T>(command, parameters);
		_dapperConnect.Connection.Close();
		return result;
	}

	#endregion

	#region WriteContext

	public async Task<T> InsertAsync(T item)
	{
		await _datasetEfContext.AddAsync(item);
		await _efContext.SaveChangesAsync();
		return item;
	}

	public async Task<T?> UpdateAsync(Guid id, T item)
	{
		var result = await _datasetEfContext.AsNoTracking().FirstOrDefaultAsync(item => item.Equals(id));

		if (result == null) return null;

		_efContext.Entry(result).CurrentValues.SetValues(item);
		await _efContext.SaveChangesAsync();

		return item;
	}

	public async Task<bool> DeleteAsync(Guid id)
	{
			var result = await _datasetEfContext.AsNoTracking().FirstOrDefaultAsync(item => item.Id.Equals(id));

			if (result is null) return false;

			_datasetEfContext.Remove(result);
			await _efContext.SaveChangesAsync();
			return true;
	}

	#endregion
}
