namespace AppManager.Domain.Dtos.Common;

public class PaginationRequest
{
	public int CurrentPage { get; private set; }
	public int ItemsPerPage { get; private set; }

	public PaginationRequest(
		int? currentPage, 
		int? itemsPerPage
	)
	{
		CurrentPage = currentPage ?? 1;
		ItemsPerPage = itemsPerPage ?? 10;
	}

	public void Deconstruct(out int? currentPage, out int? itemsPerPage)
	{
		currentPage = CurrentPage;
		itemsPerPage = ItemsPerPage;
	}
}
