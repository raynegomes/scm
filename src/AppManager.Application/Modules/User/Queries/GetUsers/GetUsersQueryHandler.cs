using AppManager.Application.Commons.Interfaces;
using AppManager.Domain.Dtos.User;
using AppManager.Domain.Entities;
using AppManager.Domain.Interfaces.Repositories;
using AppManager.Application.Commons.Helpers;
using AppManager.Domain.Dtos.Common;

namespace AppManager.Application.Modules.User.Queries.GetUsers;

public class GetUsersQueryHandler : 
	IQueryHandler<PaginationRequest, 
	GetPaginatedListResponse<GetUserDetailResponse>>
{
	private readonly string _tableNameWithSchema;
	private readonly IUserRepository _userRepository;

	public GetUsersQueryHandler(
		IUserRepository userRepository
	)
	{
		_userRepository = userRepository;
		_tableNameWithSchema = userRepository.GetTableNameWithSchema();
	}

	public async Task<GetPaginatedListResponse<GetUserDetailResponse>> Handle(PaginationRequest pagination)
	{
		var (currentPage, itemsPerPage) = pagination;
		currentPage ??= 1;
		var totalItems = await _userRepository.Count();
		var totalPages = Pagination.CalculatePagesCount(totalItems, pagination.ItemsPerPage);

		var sqlQuery = @$"
				SELECT 
						* 
				FROM 
						{_tableNameWithSchema} 
				ORDER BY 
						name
				OFFSET @Offset
				LIMIT @itemsPerPage";

		var parameters = new 
		{
			itemsPerPage,
			Offset = (currentPage - 1) * itemsPerPage,
		};

		var userList = await _userRepository.GetAsync(sqlQuery, parameters) as List<UserEntity>;
		var convertedList = userList.ConvertAll(c => new GetUserDetailResponse
		{
			Id = c.Id,
			Name = c.Name,
			Status = c.Status,
			IsEnable = c.IsEnable,
			CreatedAt = c.CreatedAt,
			UpdatedAt = c.UpdatedAt
		});

		return new GetPaginatedListResponse<GetUserDetailResponse>
		{
			TotalItems = totalItems,
			CurrentPage = (int)currentPage,
			TotalPages = totalPages,
			Data = convertedList
		};
	}
}
