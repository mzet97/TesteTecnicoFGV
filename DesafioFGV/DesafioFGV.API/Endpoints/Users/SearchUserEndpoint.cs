using DesafioFGV.API.Common.Api;
using DesafioFGV.Application.UseCases.Users.Queries;
using DesafioFGV.Application.UseCases.Users.ViewModels;
using DesafioFGV.Domain.Shared.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DesafioFGV.API.Endpoints.Users;

public class SearchUserEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
      => app.MapGet("/", HandleAsync)
          .WithName("Search users")
          .WithSummary("Busca usuarios")
          .WithDescription("Busca usuarios. Cache de 1 minutos.")
          .WithOrder(2)
          .Produces<BaseResultList<UserViewModel>>()
          .CacheOutput("Short");

    private static async Task<IResult> HandleAsync(
        IMediator mediator,
        [AsParameters] SearchUser search)
    {
        var query = new SearchUserQuery
        {
            Name = search.Name ?? "",
            Email = search.Email ?? "",
            Id = search.Id ?? Guid.Empty,
            CreatedAt = search.CreatedAt ?? default,
            Order = search.Order ?? "",
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

public class SearchUser
{
    [FromQuery] public string? Name { get; set; }
    [FromQuery] public string? Email { get; set; }
    [FromQuery] public Guid? Id { get; set; }
    [FromQuery] public DateTime? CreatedAt { get; set; }
    [FromQuery] public string? Order { get; set; }
    [FromQuery] public int? PageIndex { get; set; } = 1;
    [FromQuery] public int? PageSize { get; set; } = 10;
}