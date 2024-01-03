namespace AppManager.Application.Commons.Interfaces;

public interface ICommandHandler<TRequest, TResponse>
{
	Task<TResponse> Handle(TRequest command);
}
