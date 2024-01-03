namespace AppManager.Domain.Dtos.Common;

public class GetPaginatedListResponse<T>
{
	public int TotalItems { get; set; }
	public int TotalPages { get; set; }
	public int CurrentPage { get; set; }
	public List<T> Data { get; set; }
}
