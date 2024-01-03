using System.Text.Json;
using Microsoft.Extensions.Logging;

using AppManager.Application.Commons.Interfaces;
using AppManager.Application.Modules.User.Notifications;
using AppManager.Application.Commons.Helpers;

namespace AppManager.Application.Modules.User.EventsHandlers;

public class UpdateUserEventHandler : INotificationHandler<UpdateUserNotification>
{
	private readonly ILogger<UpdateUserEventHandler> _logger;

	public UpdateUserEventHandler(ILogger<UpdateUserEventHandler> logger)
	{
		_logger = logger;
	}

	public void Publish(UpdateUserNotification notification)
	{
		var message = notification.Concluded ?
			"USER UPDATED SUCCESSFULLY: " :
			"FAILED TO UPDATE USER: ";

		message += JsonSerializer.Serialize(notification);

		Notification.Publish(_logger, message);
	}

	public void Error(Exception exception)
	{
		Notification.Error(_logger, $"ERROR TO UPDATE USER", exception);
	}
}
