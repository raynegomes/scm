using System.Text.Json.Serialization;

using AppManager.Application.Commons.Interfaces;

namespace AppManager.Application.Modules.User.Notifications;

public class CreateUserNotification : INotification
{
	public Guid Id { get; set; }
	public string Name { get; set; }

	public bool IsEnable { get; set; }

	[JsonIgnore]
	public bool Concluded { get; set; } = false;
}
