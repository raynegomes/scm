using AppManager.Application.Commons.Interfaces;
using AppManager.Domain.Interfaces.Repositories;

using Microsoft.Extensions.Logging;

namespace AppManager.Application.Modules.User.Commands.DeleteUser;

public class DeleteUserCommandHandler : ICommandHandler<Guid, bool>
{
	private readonly IUserRepository _userRepository;
	private readonly ILogger _logger;
	public DeleteUserCommandHandler(
		IUserRepository userRepository,
		ILogger<DeleteUserCommandHandler> logger
	)
	{
		_userRepository = userRepository;
		_logger = logger;
	}
	public async Task<bool> Handle(Guid command)
	{
		try
		{
			if(await _userRepository.DeleteAsync(command))
			{
				_logger.LogInformation("User deleted successfully: {@Command}", command);
				// await _mediator.Publish(notification);
			}
			else
			{
				_logger.LogInformation("User not found to delete with ID: {@Command}", command);
			}

			return true;
		}
		catch (Exception ex)
		{
			var message = $"Error to delete user with ID: {command}";
			_logger.LogError(ex, message);
			//await _mediator.Publish(new ErrorNotification
			//{
			//	Message = ex.Message,
			//	StackTrace = ex.StackTrace ?? string.Empty
			//});
			return false;
		}
	}
}
