using DesafioFGV.API.Common.Api;
using DesafioFGV.Application.UseCases.Users.Commands;
using DesafioFGV.Domain.Shared.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DesafioFGV.API.Endpoints.Users;

public class DeleteUserEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    => app.MapDelete("/{id}", HandleAsync)
        .WithName("Delete user")
        .WithSummary("Deleta um usuario")
        .WithDescription("Deleta um usuario")
        .WithOrder(5)
        .Produces<BaseResult>();

    private static async Task<IResult> HandleAsync(
        IMediator mediator,
         [FromRoute] Guid id)
    {
        if (id == null || Guid.Empty == id)
        {
            return TypedResults.BadRequest(new BaseResult(false, "Nenhum ID foi fornecido."));
        }

        var result = await mediator.Send(new DeleteUserCommand(id));

        if (result.Success)
        {
            return TypedResults.Ok(result);
        }

        return TypedResults.BadRequest(result);
    }
}