using Microsoft.Extensions.Logging;

using AppManager.Application.Commons.Interfaces;
using AppManager.Application.Modules.User.Notifications;
using AppManager.Application.Commons.Helpers;

namespace AppManager.Application.Modules.User.EventsHandlers;

public class DeleteUserEventHandler : INotificationHandler<DeleteUserNotification>
{
	private readonly ILogger<DeleteUserEventHandler> _logger;

	public DeleteUserEventHandler(ILogger<DeleteUserEventHandler> logger)
	{
		_logger = logger;
	}

	public void Publish(DeleteUserNotification notification)
	{
		var message = notification.Concluded ?
			"USER DELETED: id => " :
			"FAILED TO DELETE USER: id => ";

		message += $"{notification.Id};";

		Notification.Publish(_logger, message);
	}
	public void Error(Exception exception)
	{
		Notification.Error(_logger, $"ERROR TO DELETE USER", exception);
	}
}
