using DesafioFGV.API.Common.Api;
using DesafioFGV.Application.UseCases.Users.Commands;
using DesafioFGV.Application.UseCases.Users.ViewModels;
using DesafioFGV.Domain.Shared.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DesafioFGV.API.Endpoints.Users;

public class UpdateUserEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPut("/{id}", HandleAsync)
            .WithName("Update user")
            .WithSummary("Atualiza um usuario")
            .WithDescription("Atualiza um usuario")
            .WithOrder(6)
            .Produces<BaseResult<UserViewModel>>();

    private static async Task<IResult> HandleAsync(
        IMediator mediator,
        [FromRoute] Guid id,
        [FromBody] UpdateUserCommand command)
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