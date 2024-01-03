using System.Net;
using FluentValidation;

namespace AppManager.API.Filter;

public class ValidationFilter<T> : IEndpointFilter
{
	public async ValueTask<object?> InvokeAsync(
		EndpointFilterInvocationContext context, 
		EndpointFilterDelegate next
	)
	{
		var argToValidate = context.GetArgument<T>(0);
		var validator = context.HttpContext.RequestServices.GetService<IValidator<T>>();

		if (validator is not null) 
		{
			var validationResult = await validator.ValidateAsync(argToValidate!);
			if (!validationResult.IsValid) 
			{
				return Results.ValidationProblem(validationResult.ToDictionary(),
					statusCode: (int)HttpStatusCode.UnprocessableEntity);
			}
		}

		return await next.Invoke(context);
	}
}
