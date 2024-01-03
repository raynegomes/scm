namespace AppManager.Application.Commons.Interfaces;

public interface IQueryHandler<TResponse>
{
	Task<TResponse> Handle();
}

public interface IQueryHandler<TRequest, TResponse>
{
	Task<TResponse> Handle(TRequest request);
}
