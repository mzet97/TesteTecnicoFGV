using DesafioFGV.Domain.Entities;
using DesafioFGV.Domain.Entities.Validations;
using DesafioFGV.Domain.Exceptions;
using DesafioFGV.Domain.Repositories;
using DesafioFGV.Domain.Services.Interface;
using DesafioFGV.Domain.Shared.Responses;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DesafioFGV.Application.UseCases.Orders.Commands.Handlers;

public class CreateOrderCommandHandler(
    IOrderRepository orderRepository,
    IApplicationUserService applicationUserService,
    ILogger<CreateOrderCommandHandler> logger
    ) : IRequestHandler<CreateOrderCommand, BaseResult<Guid>>
{
    public async Task<BaseResult<Guid>> Handle(
        CreateOrderCommand request, 
        CancellationToken cancellationToken)
    {
        var order = new Order
        {
            Description = request.Description,
            Value = request.Value,
            DateOrder = request.DateOrder,
            IdUser = request.IdUser
        };

        var validator = new OrderValidation();
        var result = validator.Validate(order);

        if (!result.IsValid)
        {
            throw new ValidationException(result.Errors);
        }

        var user = await applicationUserService.FindByIdAsync(request.IdUser);

        if(user == null)
        {
            return new BaseResult<Guid>(Guid.Empty, false, "User not found.");
        }

        await orderRepository.AddAsync(order);

        return new BaseResult<Guid>(order.Id, true, "Order created successfully.");
    }
}
