using AutoMapper;

using AppManager.Application.Modules.User.Notifications;
using AppManager.Domain.Entities;
using AppManager.Domain.Dtos.User;

namespace AppManager.Application.Mapper;

public class UserMapper : Profile
{
	public UserMapper()
	{
		CreateMap<GetUserDetailResponse, UserEntity>()
			.ConstructUsing(c =>
				new UserEntity(c.Name, c.IsEnable)
			).ReverseMap();
		
		CreateMap<CreateUserRequest, UserEntity>()
			.ConstructUsing(c =>
				new UserEntity(c.Name, c.IsEnable)
			).ReverseMap();

		CreateMap<CreateUserResponse, UserEntity>()
			.ConstructUsing(c =>
				new UserEntity(c.Name, c.IsEnable)
			).ReverseMap();
		
		CreateMap<CreateUserNotification, UserEntity>()
			.ConstructUsing(c =>
				new UserEntity(c.Name, c.IsEnable)
			).ReverseMap();
	}
}
