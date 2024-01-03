namespace AppManager.Domain.Dtos.Common;

public class ResultEntity<T>
{
	public int StatusCode { get; }
	public string? Message { get; }
	public T? Data { get; }

	public ResultEntity(
		int statusCode,
		string? message,
		T? data)
	{
		StatusCode = statusCode;
		Data = data;
		Message = message;
	}
}
