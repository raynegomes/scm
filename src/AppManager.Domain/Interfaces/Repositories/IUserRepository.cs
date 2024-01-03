using AppManager.Domain.Entities;

namespace AppManager.Domain.Interfaces.Repositories;

public interface IUserRepository : IBaseRepository<UserEntity>
{
	Task<UserEntity?> GetByEmailAsync(string email);

	Task<UserEntity?> GetByUsernameAsync(string username);
}
