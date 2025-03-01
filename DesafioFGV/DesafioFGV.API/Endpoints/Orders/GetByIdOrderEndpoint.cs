using DesafioFGV.API.Common.Api;
using DesafioFGV.Application.UseCases.Orders.Queries;
using DesafioFGV.Application.UseCases.Orders.ViewModels;
using DesafioFGV.Domain.Shared.Responses;
using MediatR;

namespace DesafioFGV.API.Endpoints.Orders;

public class GetByIdOrderEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    => app.MapGet("/{id}", HandleAsync)
        .WithName("Get order by id")
        .WithSummary("Obtem pedido pelo id")
        .WithDescription("Obtem pedido pelo id. Cache de 1 minutos")
        .WithOrder(2)
        .Produces<BaseResult<OrderViewModel>>()
        .CacheOutput("Short");

    private static async Task<IResult> HandleAsync(
        IMediator mediator,
        Guid id)
    {
        var result = await mediator.Send(new GetByIdOrderQuery(id));

        if (result.Success)
        {
            return TypedResults.Ok(result);
        }

        return TypedResults.BadRequest(result);
    }
}