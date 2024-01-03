using AppManager.Application.Modules.User.DomainServices.Interfaces;
using AppManager.Domain.Interfaces.Repositories;

namespace AppManager.Application.Modules.User.DomainServices;

public class CheckIsUniqueUser : ICheckIsUniqueUser
{
	private readonly IUserRepository _repository;

	public CheckIsUniqueUser(IUserRepository repository)
	{
		_repository = repository;
	}
	public bool EmailExist(string email)
	{
		var user = _repository.GetByEmailAsync(email);
		return user is not null;
	}

	public bool UsernameExist(string username)
	{
		var user = _repository.GetByUsernameAsync(username);
		return user is not null;
	}
}
