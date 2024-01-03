namespace AppManager.Domain.Entities;

public abstract class BaseEntity
{
	public Guid Id { get; private set; }

	public bool IsEnable { get; private set; }

	public DateTime CreatedAt { get; private set; }

	public DateTime? UpdatedAt { get; private set; }

	public BaseEntity(bool isEnable = true)
	{
		Id = Guid.NewGuid();
		CreatedAt = DateTime.UtcNow;
		IsEnable = isEnable;
	}

	public virtual void Enable()
	{
		IsEnable = true;
		SetUpdateDate();
	}

	public virtual void Disable()
	{
		IsEnable = false;
		SetUpdateDate();
	}

	private protected void SetUpdateDate()
	{
		UpdatedAt = DateTime.UtcNow;
	}
}
