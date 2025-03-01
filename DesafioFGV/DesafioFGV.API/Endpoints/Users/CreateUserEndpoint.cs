using DesafioFGV.API.Common.Api;
using DesafioFGV.Application.UseCases.Users.Commands;
using DesafioFGV.Domain.Shared.Responses;
using MediatR;

namespace DesafioFGV.API.Endpoints.Users;

public class CreateUserEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
         => app.MapPost("/", HandleAsync)
             .WithName("Create a new user")
             .WithSummary("Criar um novo usuario")
             .WithDescription("Criar um novo usuario")
             .WithOrder(1)
             .Produces<BaseResult<Guid>>();

    private static async Task<IResult> HandleAsync(
        IMediator mediator,
        CreateUserCommand command)
    {

        var result = await mediator.Send(command);

        if (result.Success)
        {
            return TypedResults.Ok(result);
        }

        return TypedResults.BadRequest(result);
    }
}
