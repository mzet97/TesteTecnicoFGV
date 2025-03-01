using DesafioFGV.API.Common.Api;
using DesafioFGV.Application.UseCases.Orders.Commands;
using DesafioFGV.Domain.Shared.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DesafioFGV.API.Endpoints.Orders;

public class DeleteOrderEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    => app.MapDelete("/{id}", HandleAsync)
        .WithName("Delete order")
        .WithSummary("Deleta um pedido")
        .WithDescription("Deleta um pedido")
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

        var result = await mediator.Send(new DeleteOrderCommand(id));

        if (result.Success)
        {
            return TypedResults.Ok(result);
        }

        return TypedResults.BadRequest(result);
    }
}
