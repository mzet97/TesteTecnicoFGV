using DesafioFGV.Application.UseCases.Orders.ViewModels;
using DesafioFGV.Domain.Shared.Responses;
using MediatR;

namespace DesafioFGV.Application.UseCases.Orders.Queries;

public class GetByIdOrderQuery : IRequest<BaseResult<OrderViewModel>>
{
    public GetByIdOrderQuery(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; set; }
}

