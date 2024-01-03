namespace AppManager.Application.Commons.Helpers;

public static class Pagination
{
	public static int CalculatePagesCount(
		int totalItems,
		int itemsPerPage
	)
	{
		return (int)(totalItems <= itemsPerPage ? 1 : (totalItems / itemsPerPage));
	}
}
