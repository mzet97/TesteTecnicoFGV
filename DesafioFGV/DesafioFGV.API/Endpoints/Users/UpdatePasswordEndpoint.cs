using DesafioFGV.API.Common.Api;
using DesafioFGV.Application.UseCases.Users.Commands;
using DesafioFGV.Domain.Shared.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DesafioFGV.API.Endpoints.Users;

public class UpdatePasswordEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPatch("/{id}", HandleAsync)
            .WithName("Update password user")
            .WithSummary("Atualiza a senha de um usuario")
            .WithDescription("Atualiza a senha de um usuario")
            .WithOrder(4)
            .Produces<BaseResult>();

    private static async Task<IResult> HandleAsync(
        IMediator mediator,
        [FromRoute] Guid id,
        [FromBody] UpdatePasswordCommand command)
    {

        if (id != command.Id)
        {
            return TypedResults.BadRequest("Id da rota e Id do corpo da requisição não são iguais");
        }

        var result = await mediator.Send(command);

        if (result.Success)
        {
            return TypedResults.Ok(result);
        }

        return TypedResults.BadRequest(result);
    }
}