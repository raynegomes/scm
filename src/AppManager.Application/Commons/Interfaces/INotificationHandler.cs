namespace AppManager.Application.Commons.Interfaces;

public interface INotificationHandler<T> where T : INotification
{
	void Publish(T notification);
	void Error(Exception exception);
}
