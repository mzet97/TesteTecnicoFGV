using DesafioFGV.API.Common.Api;
using DesafioFGV.Application.UseCases.Users.Queries;
using DesafioFGV.Application.UseCases.Users.ViewModels;
using DesafioFGV.Domain.Shared.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DesafioFGV.API.Endpoints.Users;

public class GetByIdUserEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    => app.MapGet("/{id:guid}", HandleAsync)
        .WithName("Obtem user pelo id")
        .WithSummary("Obtem user pelo id")
        .WithDescription("Obtem user pelo id. Cache de 1 minutos.")
        .WithOrder(3)
        .Produces<BaseResult<UserViewModel>>()
        .CacheOutput("Short");

    private static async Task<IResult> HandleAsync(
        IMediator mediator,
        [FromRoute] Guid id)
    {
        var result = await mediator.Send(new GetByIdUserQuery(id));

        if (result.Success)
        {
            return TypedResults.Ok(result);
        }

        return TypedResults.BadRequest(result);
    }
}