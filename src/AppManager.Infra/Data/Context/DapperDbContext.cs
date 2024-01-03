using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data;

namespace AppManager.Infra.Data.Context;

public sealed class DapperDbContext : IDisposable
{
	private readonly IConfiguration _configuration;

	public IDbConnection Connection { get; }

	public DapperDbContext(IConfiguration config)
	{
		_configuration = config;
		Connection = new NpgsqlConnection(_configuration.GetConnectionString("QueriesConnectionStrings"));
		Connection.Open();
	}

	public void Close() => Connection?.Close();

	public void Dispose() => Connection?.Dispose();

}
