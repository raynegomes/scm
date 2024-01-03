using AppManager.Application.Commons.Interfaces;
using AppManager.Application.Modules.User.Commands.CreateUser;
using AppManager.Application.Modules.User.Commands.DeleteUser;
using AppManager.Application.Modules.User.EventsHandlers;
using AppManager.Application.Modules.User.Notifications;
using AppManager.Application.Modules.User.Queries.GetUserDetail;
using AppManager.Application.Modules.User.Queries.GetUsers;
using AppManager.Domain.Dtos.Common;
using AppManager.Domain.Dtos.User;
using AppManager.Domain.Interfaces.Repositories;
using AppManager.Infra.Data.Repositories;

using FluentValidation;

namespace AppManager.API.Configurations.DependencyInjection;

public static class UserServicesInjection
{
	public static void AddUserServicesInjection(this IServiceCollection services)
	{
		#region Repository
		
		services.AddTransient<IUserRepository, UserRepository>();
		
		#endregion

		#region Commands

		services.AddTransient<ICommandHandler<CreateUserRequest, CreateUserResponse?>, CreateUserCommandHandler>();
		services.AddTransient<ICommandHandler<Guid, bool>, DeleteUserCommandHandler>();

		#endregion

		#region Querys

		services.AddTransient<IQueryHandler<Guid, GetUserDetailResponse>, GetUserDetailQueryHandler>();
		services.AddTransient<IQueryHandler<PaginationRequest, GetPaginatedListResponse<GetUserDetailResponse>>, GetUsersQueryHandler>();

		#endregion

		#region Notifications

		services.AddScoped<INotificationHandler<CreateUserNotification>, CreateUserEventHandler>();
		services.AddScoped<INotificationHandler<UpdateUserNotification>, UpdateUserEventHandler>();
		services.AddScoped<INotificationHandler<DeleteUserNotification>, DeleteUserEventHandler>();

		#endregion

		#region Validation

		services.AddScoped<IValidator<CreateUserRequest>, CreateUserValidator>();

		#endregion
	}
}
