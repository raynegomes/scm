using Microsoft.Extensions.Logging;
using System.Text.Json;

using AppManager.Application.Commons.Interfaces;
using AppManager.Application.Modules.User.Notifications;
using AppManager.Application.Commons.Helpers;

namespace AppManager.Application.Modules.User.EventsHandlers;

public class CreateUserEventHandler : INotificationHandler<CreateUserNotification>
{
	private readonly ILogger<CreateUserEventHandler> _logger;

	public CreateUserEventHandler(ILogger<CreateUserEventHandler> logger)
	{
		_logger = logger;
	}

	public void Publish(CreateUserNotification notification)
	{
		var message = $"USER CREATED SUCCESSFULLY: {JsonSerializer.Serialize(notification)}";
		Notification.Publish(_logger, message);
	}

	public void Error(Exception exception)
	{
		var message = $"ERROR TO CREATE USER";
		Notification.Error(_logger, message, exception);
	}
}
