using DesafioFGV.Application.UseCases.Orders.ViewModels;
using DesafioFGV.Domain.Entities;
using DesafioFGV.Domain.Shared.Responses;
using MediatR;

namespace DesafioFGV.Application.UseCases.Orders.Commands;

public class UpdateOrderCommand : IRequest<BaseResult<OrderViewModel>>
{
    public required string Description { get; set; }
    public required decimal Value { get; set; }
    public required DateTime DateOrder { get; set; }

    public required Guid IdUser { get; set; }
    public required Guid Id { get; set; }

    public Order ToDomain()
    {
        var entity = new Order();
        entity.Description = Description;
        entity.Value = Value;
        entity.DateOrder = DateOrder;
        entity.IdUser = IdUser;
        entity.Id = Id;
        return entity;
    }
}
