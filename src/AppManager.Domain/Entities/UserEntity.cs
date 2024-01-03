using AppManager.Domain.Enums;

namespace AppManager.Domain.Entities;

public class UserEntity: BaseEntity
{
	public string Name { get; private set; }
	public UserStatus Status { get; private set; }

	private UserEntity() : base(true)
	{
		Status = UserStatus.Active;
	}

	public UserEntity(string name, bool isEnable)
		: base(isEnable)
	{
		Name = name;
		Status = UserStatus.Active;
	}
	public void ChangeName(string name)
	{
		Name = name;
		SetUpdateDate();
	}

	public void ChangeStatus(UserStatus status)
	{
		Status = status;
		SetUpdateDate();
	}
}
