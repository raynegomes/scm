using Dapper;

using AppManager.Domain.Entities;
using AppManager.Domain.Interfaces.Repositories;
using AppManager.Infra.Data.Context;
using AppManager.Infra.Data.Mapping;



namespace AppManager.Infra.Data.Repositories
{
	public class UserRepository : BaseRepository<UserEntity>, IUserRepository
	{
		public UserRepository(
			EfDbContext context, 
			DapperDbContext readContext
		) 
			: base(
					context, 
					readContext,
					SchemaNames.DefaultSchema,
					TableNames.UserTable
			)
		{
		}

		public async Task<UserEntity?> GetByEmailAsync(string email)
		{
			using var connection = _dapperConnect.Connection;
			var query = $@"SELECT * FROM {_tableNameWithSchema} WHERE email = @email";
			var parameters = new DynamicParameters(new { email });
			return await connection.QueryFirstOrDefaultAsync<UserEntity>(query, parameters);
		}

		public async Task<UserEntity?> GetByUsernameAsync(string username)
		{
			using var connection = _dapperConnect.Connection;
			var query = $@"SELECT * FROM {_tableNameWithSchema} WHERE username = @username";
			var parameters = new DynamicParameters(new { username });
			return await connection.QueryFirstOrDefaultAsync<UserEntity>(query, parameters);
		}
	}
}
