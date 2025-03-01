using DesafioFGV.Domain.Shared.Responses;
using MediatR;

namespace DesafioFGV.Application.UseCases.Orders.Commands;

public class DeleteOrderCommand : IRequest<BaseResult>
{
    public DeleteOrderCommand(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; set; }
}
