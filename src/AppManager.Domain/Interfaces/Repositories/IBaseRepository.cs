using AppManager.Domain.Entities;

namespace AppManager.Domain.Interfaces.Repositories;

public interface IBaseRepository<T> where T : BaseEntity
{
	string GetTableNameWithSchema();
	#region ReadContext
	
	Task<bool> ExistAsync(Guid id);
	Task<T?> GetByIdAsync(Guid id);
	Task<IEnumerable<T>> GetAsync(string command, object? parameters);
	Task<int> Count();

	#endregion

	#region WriteContext

	Task<T> InsertAsync(T item);
	Task<T?> UpdateAsync(Guid id, T item);
	Task<bool> DeleteAsync(Guid id);

	#endregion
}
