namespace AppManager.Domain.Interfaces.Repositories
{
	public interface ICacheRepository
	{
		Task<string?> GetValue(string keyName, string? prefix);

		void SetValue(string keyName, string keyValue, string? prefix, int expirationTImeInMinute);

		void DeleteValue(string keyName, string? prefix);
	}
}
