using AppManager.Application.Commons.Interfaces;

namespace AppManager.Application.Modules.User.Notifications;

public class DeleteUserNotification : INotification
{
	public Guid Id { get; set; }
	public bool Concluded { get; set; }
}
