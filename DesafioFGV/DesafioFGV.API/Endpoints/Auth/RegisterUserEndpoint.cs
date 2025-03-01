using DesafioFGV.API.Common.Api;
using DesafioFGV.Application.UseCases.Auth.Commands;
using DesafioFGV.Application.UseCases.Auth.ViewModels;
using DesafioFGV.Domain.Shared.Responses;
using MediatR;

namespace DesafioFGV.API.Endpoints.Auth;

public class RegisterUserEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
         => app.MapPost("/register", HandleAsync)
             .WithName("Register: Register a new user")
             .WithSummary("Criar um novo usuario")
             .WithDescription("Criar um novo usuario")
             .WithOrder(2)
             .Produces<BaseResult<LoginResponseViewModel?>>();

    private static async Task<IResult> HandleAsync(
        IMediator mediator,
        RegisterUserCommand command)
    {

        var result = await mediator.Send(command);

        if (result.Success)
        {
            return TypedResults.Ok(result);
        }

        return TypedResults.BadRequest(result);
    }
}
