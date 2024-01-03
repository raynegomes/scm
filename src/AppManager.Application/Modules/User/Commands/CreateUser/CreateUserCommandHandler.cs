using AutoMapper;

using AppManager.Domain.Entities;
using AppManager.Domain.Interfaces.Repositories;
using AppManager.Application.Commons.Interfaces;
using AppManager.Domain.Dtos.User;
using AppManager.Application.Modules.User.Notifications;

namespace AppManager.Application.Modules.User.Commands.CreateUser;

public class CreateUserCommandHandler : ICommandHandler<CreateUserRequest, CreateUserResponse?>
{
	private readonly IUserRepository _userRepository;
	private readonly IMapper _mapper;
	private readonly INotificationHandler<CreateUserNotification> _notificationHandler;

	public CreateUserCommandHandler(
		IUserRepository userRepository,
		IMapper mapper,
		INotificationHandler<CreateUserNotification> notificationHandler
	)
	{
		_userRepository = userRepository;
		_mapper = mapper;
		_notificationHandler = notificationHandler;
	}

	public async Task<CreateUserResponse?> Handle(CreateUserRequest request)
	{
		var user = _mapper.Map<CreateUserRequest, UserEntity>(request);
		var userNotification = _mapper.Map<UserEntity, CreateUserNotification>(user);

		try
		{
			user = await _userRepository.InsertAsync(user);

			var returnData = _mapper.Map<UserEntity, CreateUserResponse>(user);
			userNotification.Concluded = true;

			_notificationHandler.Publish(userNotification);
			return returnData;
		}
		catch (Exception ex)
		{
			_notificationHandler.Error(ex);			
		}
		return null;
	}
}
