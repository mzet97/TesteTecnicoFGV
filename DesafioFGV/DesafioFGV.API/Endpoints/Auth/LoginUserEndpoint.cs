using DesafioFGV.API.Common.Api;
using DesafioFGV.Application.UseCases.Auth.Commands;
using DesafioFGV.Application.UseCases.Auth.ViewModels;
using DesafioFGV.Domain.Shared.Responses;
using MediatR;

namespace DesafioFGV.API.Endpoints.Auth;

public class LoginUserEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("/login", HandleAsync)
            .WithName("Login: login in application")
            .WithSummary("Faz o login")
            .WithDescription("Faz o login")
            .WithOrder(1)
            .Produces<BaseResult<LoginResponseViewModel?>>();

    private static async Task<IResult> HandleAsync(
        IMediator mediator,
        LoginUserCommand command)
    {

        var result = await mediator.Send(command);

        if (result.Success)
        {
            return TypedResults.Ok(result);
        }

        return TypedResults.BadRequest(result);
    }
}
