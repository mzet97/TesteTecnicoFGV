using DesafioFGV.API.Common.Api;
using DesafioFGV.Application.UseCases.Orders.Commands;
using DesafioFGV.Application.UseCases.Orders.ViewModels;
using DesafioFGV.Domain.Shared.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DesafioFGV.API.Endpoints.Orders;

public class UpdateOrderEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPut("/{id}", HandleAsync)
            .WithName("Update order")
            .WithSummary("Atualiza um pedido")
            .WithDescription("Atualiza um pedido")
            .WithOrder(6)
            .Produces<BaseResult<OrderViewModel>>();

    private static async Task<IResult> HandleAsync(
        IMediator mediator,
        [FromRoute] Guid id,
        [FromBody] UpdateOrderCommand command)
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
