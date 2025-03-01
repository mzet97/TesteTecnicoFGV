using DesafioFGV.API.Common.Api;
using DesafioFGV.Application.UseCases.Orders.Commands;
using DesafioFGV.Domain.Shared.Responses;
using MediatR;

namespace DesafioFGV.API.Endpoints.Orders;

public class CreateOrderEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    => app.MapPost("/", HandleAsync)
        .WithName("Create a mew order")
        .WithSummary("Criar um novo pedido")
        .WithDescription("Criar um novo pedido")
        .WithOrder(1)
        .Produces<BaseResult<Guid>>();

    private static async Task<IResult> HandleAsync(
        IMediator mediator,
        CreateOrderCommand command)
    {

        var result = await mediator.Send(command);

        if (result.Success)
        {
            return TypedResults.Ok(result);
        }

        return TypedResults.BadRequest(result);
    }
}