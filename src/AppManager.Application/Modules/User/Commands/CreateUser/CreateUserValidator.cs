using FluentValidation;

using AppManager.Domain.Dtos.User;

namespace AppManager.Application.Modules.User.Commands.CreateUser;

public class CreateUserValidator : AbstractValidator<CreateUserRequest>
{
	public CreateUserValidator()
	{
		RuleFor(u => u.Name)
			.NotNull()
			.NotEmpty()
			.MinimumLength(3)
			.WithMessage("the name must be at least 3 characters long");
	}
}
