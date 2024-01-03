using System.Text.Json.Serialization;

using AppManager.Application.Commons.Interfaces;

namespace AppManager.Application.Modules.User.Notifications;

public class UpdateUserNotification : INotification
{
	public Guid Id { get; set; }

	[JsonIgnore]
	public bool Concluded { get; set; }
}
