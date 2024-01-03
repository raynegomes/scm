using System.Text;
using Microsoft.Extensions.Logging;

namespace AppManager.Application.Commons.Helpers;

public static class Notification
{

	public static void Publish(this ILogger logger, string message)
	{
		logger.LogInformation(message);
		// Task.Run(() => Console.WriteLine(message));
	}
	public static void Error(this ILogger logger, string message, Exception exception)
	{
		var msg = new StringBuilder();
		msg.Append("APPLICATION ERROR:\n");
		msg.Append($"Error Message: {message} \n");
		msg.Append($"Exception: {exception.Message} \\n {exception.StackTrace} \n");
		logger.LogError(msg.ToString());
		// Task.Run(() => Console.WriteLine(msg.ToString()));
	}
}
