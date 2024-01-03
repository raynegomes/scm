using Carter;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;

using AppManager.API.Filter;
using AppManager.Application.Modules.User.Notifications;
using AppManager.Application.Commons.Interfaces;
using AppManager.Domain.Dtos.User;
using AppManager.Domain.Dtos.Common;

namespace AppManager.API.Routes.User;

public class UserRoutes : CarterModule
{
	private const string PREFIX_ROUTE = "/api/users";
	private const string TAG_ROUTE = "User";


	public UserRoutes()
		:base(PREFIX_ROUTE)
	{
		WithTags(TAG_ROUTE);
	}

	public override void AddRoutes(IEndpointRouteBuilder router)
	{	
		router.MapGet("/", GetAllUsers)
			.Produces<GetPaginatedListResponse<GetUserDetailResponse>>(StatusCodes.Status200OK)
			.Produces(StatusCodes.Status500InternalServerError)
			.WithName("GetUsers");

		router.MapGet("/{id}", GetUserById)
			.Produces<GetUserDetailResponse>(StatusCodes.Status200OK)
			.Produces(StatusCodes.Status404NotFound)
			.Produces(StatusCodes.Status500InternalServerError)
			.WithName("GetUserById");

		router.MapPost("/", CreateUser)
			.AddEndpointFilter<ValidationFilter<CreateUserRequest>>()
			.Produces<CreateUserNotification>(StatusCodes.Status201Created)
			.Produces(StatusCodes.Status400BadRequest)
			.Produces(StatusCodes.Status422UnprocessableEntity)
			.Produces(StatusCodes.Status500InternalServerError)
			.WithName("CreatetUser");

		router.MapPut("/{id}", () => { })
			.AddEndpointFilter<ValidationFilter<CreateUserRequest>>()
			.Produces(StatusCodes.Status204NoContent)
			.Produces(StatusCodes.Status400BadRequest)
			.Produces(StatusCodes.Status422UnprocessableEntity)
			.Produces(StatusCodes.Status500InternalServerError)
			.WithName("UpdatetUser");

		router.MapDelete("/{id}", DeleteUser)
			.Produces(StatusCodes.Status204NoContent)
			.Produces(StatusCodes.Status400BadRequest)
			.WithName("DeleteUser");
	}

	#region GET

	private async Task<IResult> GetAllUsers(
		[FromServices]IQueryHandler<PaginationRequest,GetPaginatedListResponse<GetUserDetailResponse>> handler,
		[FromQuery]int? page,
		[FromQuery]int? itemsPerPage

	)
	{
		var pagination = new PaginationRequest(page, itemsPerPage);
		var users = await handler.Handle(pagination);
		return Results.Ok(users);
	}

	private async Task<IResult> GetUserById(
		[FromRoute]Guid id,
		[FromServices]IQueryHandler<Guid,GetUserDetailResponse> handler
	)
	{
		var user = await handler.Handle(id);
		return Results.Ok(user);
	}

	#endregion

	#region POST

	private async Task<IResult> CreateUser(
		[FromBody]CreateUserRequest command,
		[FromServices]ICommandHandler<CreateUserRequest,CreateUserResponse> handler
	)
	{
		var response = await handler.Handle(command);

		return (response is not null) ?
			Results.CreatedAtRoute("GetUserById", new { id = response.Id }, response)
		: Results.BadRequest("Houve um problema ao salvar o registro");
	}

	#endregion

	#region PUT
	#endregion

	#region DELETE

	private async Task<IResult> DeleteUser(
		[FromRoute]Guid id,
		[FromServices]ICommandHandler<Guid,bool> handler
	)
	{
		var deleted = await handler.Handle(id);
		return deleted ? Results.NoContent() : Results.BadRequest();
	}

	#endregion
}