namespace AppManager.Domain.Dtos.User;

public class CreateUserRequest
{
	public string Name { get; }
	public bool IsEnable { get; }

	public CreateUserRequest(string name, bool isEnable)
	{
		Name = name;
		IsEnable = isEnable;
	}
}
