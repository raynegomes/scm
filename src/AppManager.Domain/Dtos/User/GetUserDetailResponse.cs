using AppManager.Domain.Enums;

namespace AppManager.Domain.Dtos.User;

public class GetUserDetailResponse
{
	public Guid Id { get; set; }
	public string Name { get; set; }
	public UserStatus Status { get; set; }
	public bool IsEnable { get; set; }
	public DateTime CreatedAt { get; set; }
	public DateTime? UpdatedAt { get; set; }
}
