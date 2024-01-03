using AppManager.Domain.Interfaces.Repositories;

using Microsoft.Extensions.Caching.Distributed;

namespace AppManager.Infra.Data.Repositories;

public class CacheRepository : ICacheRepository
{
	private const int SLIDING_TIME_MIN = 20;
	private readonly IDistributedCache? _cache;

	public CacheRepository(
		IDistributedCache cache
	)
	{
		_cache = cache;
	}

	public async Task<string?> GetValue(string keyName, string? prefix)
	{
		if (_cache is null) return string.Empty;

		var strPrefix = CreateKey(keyName, prefix);

		return await _cache.GetStringAsync(strPrefix);
	}
	 
	public void SetValue(string keyName, string keyValue, string? prefix, int expirationTimeInMinute = 0)
	{
		if (_cache is null) return;

		var strPrefix = CreateKey(keyName, prefix);

		if (expirationTimeInMinute == 0)
		{
			var options = new DistributedCacheEntryOptions
			{
				AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(expirationTimeInMinute),
				SlidingExpiration = TimeSpan.FromMinutes(SLIDING_TIME_MIN)
			};
			_cache.SetStringAsync(strPrefix, keyValue, options);
			return;
		}
		_cache.SetStringAsync(strPrefix, keyValue);
	}

	public void DeleteValue(string keyName, string? prefix)
	{
		if (_cache is not null)
		{
			var strPrefix = CreateKey(keyName, prefix);
			_cache.Remove(strPrefix);
		}
	}

	internal static string CreateKey(string keyName, string? prefix)
	{
		return prefix is null ? keyName : $"{prefix}:{keyName}";
	}
}

