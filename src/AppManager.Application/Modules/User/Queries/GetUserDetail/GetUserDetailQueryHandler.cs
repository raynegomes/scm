using AutoMapper;
using Microsoft.Extensions.Logging;

using AppManager.Application.Commons.Interfaces;
using AppManager.Domain.Dtos.User;
using AppManager.Domain.Entities;
using AppManager.Domain.Interfaces.Repositories;

namespace AppManager.Application.Modules.User.Queries.GetUserDetail;

public class GetUserDetailQueryHandler : IQueryHandler<Guid, GetUserDetailResponse>
{
	private readonly IUserRepository _userRepository;
	private readonly IMapper _mapper;
	private readonly ILogger<GetUserDetailQueryHandler> _logger;

	public GetUserDetailQueryHandler(
		IUserRepository userRepository,
		IMapper mapper,
		ILogger<GetUserDetailQueryHandler> logger
	)
	{
		_userRepository = userRepository;
		_mapper = mapper;
		_logger = logger;
	}
	public async Task<GetUserDetailResponse> Handle(Guid request)
	{
		try
		{
			var result = await _userRepository.GetByIdAsync(request);
			if ( result is not null ) 
			{ 
				_logger.LogInformation("User found successfully: {@userId}", result.Id);
				// await _mediator.Publish(notification);
				return _mapper.Map<UserEntity, GetUserDetailResponse>(result);
			}
			else
			{
				_logger.LogInformation("User not found with ID: {@userId}", request);
			}
		}
		catch (Exception ex)
		{
			var message = $"Error to search user with ID: {request}";
			_logger.LogError(ex, message);
			//await _mediator.Publish(new ErrorNotification
			//{
			//	Message = ex.Message,
			//	StackTrace = ex.StackTrace ?? string.Empty
			//});
		}
		return new GetUserDetailResponse();
	}
}