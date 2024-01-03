namespace AppManager.Application.Modules.User.DomainServices.Interfaces;

public interface ICheckIsUniqueUser
{
	bool EmailExist(string email);
	bool UsernameExist(string username);
}
