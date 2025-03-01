using DesafioFGV.Domain.Shared.Responses;
using MediatR;

namespace DesafioFGV.Application.UseCases.Orders.Commands;

public class CreateOrderCommand : IRequest<BaseResult<Guid>>
{
    public required string Description { get; set; }
    public required decimal Value { get; set; }
    public required DateTime DateOrder { get; set; }

    public required Guid IdUser { get; set; }
}
