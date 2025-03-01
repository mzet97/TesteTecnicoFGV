using DesafioFGV.API.Common.Api;
using DesafioFGV.Application.UseCases.Orders.Queries;
using DesafioFGV.Application.UseCases.Orders.ViewModels;
using DesafioFGV.Domain.Shared.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DesafioFGV.API.Endpoints.Orders;

public class SearchOrderEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
       => app.MapGet("/", HandleAsync)
           .WithName("Search order")
           .WithSummary("Busca pedido")
           .WithDescription("Busca pedido Cache de 1 minutos.")
           .WithOrder(2)
           .Produces<BaseResultList<OrderViewModel>>()
           .CacheOutput("Short");

    private static async Task<IResult> HandleAsync(
        IMediator mediator,
        [AsParameters] SearchOrder search)
    {
        var query = new SearchOrderQuery
        {
            Value = search.Value ?? 0,
            Description = search.Description ?? "",
            UserName = search.UserName ?? "",
            Email = search.Email ?? "",
            Id = search.Id ?? Guid.Empty,
            IdUser = search.IdUser ?? Guid.Empty,
            DateOrder = search.DateOrder ?? default,
            CreatedAt = search.CreatedAt ?? default,
            UpdatedAt = search.UpdatedAt ?? default,
            DeletedAt = search.DeletedAt ?? default,
            Order = search.Order ?? "",
            Include = search.Include ?? false,
            PageIndex = search.PageIndex ?? 1,
            PageSize = search.PageSize ?? 10,
        };

        var result = await mediator.Send(query);

        if (result.Success)
        {
            return TypedResults.Ok(result);
        }

        return TypedResults.BadRequest(result);
    }
}

public class SearchOrder
{
    [FromQuery] public decimal? Value { get; set; }
    [FromQuery] public string? Description { get; set; }
    [FromQuery] public string? UserName { get; set; }
    [FromQuery] public string? Email { get; set; }
    [FromQuery] public Guid? Id { get; set; }
    [FromQuery] public Guid? IdUser { get; set; }
    [FromQuery] public DateTime? DateOrder { get; set; }
    [FromQuery] public DateTime? CreatedAt { get; set; }
    [FromQuery] public DateTime? UpdatedAt { get; set; }
    [FromQuery] public DateTime? DeletedAt { get; set; }
    [FromQuery] public string? Order { get; set; }
    [FromQuery] public bool? Include { get; set; }
    [FromQuery] public int? PageIndex { get; set; } = 1;
    [FromQuery] public int? PageSize { get; set; } = 10;
}