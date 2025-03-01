using DesafioFGV.Application.UseCases.Orders.ViewModels;
using DesafioFGV.Domain.Entities.Validations;
using DesafioFGV.Domain.Exceptions;
using DesafioFGV.Domain.Repositories;
using DesafioFGV.Domain.Services.Interface;
using DesafioFGV.Domain.Shared.Responses;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DesafioFGV.Application.UseCases.Orders.Commands.Handlers;

public class UpdateOrderCommandHandler(
    IOrderRepository orderRepository,
    IApplicationUserService applicationUserService,
    ILogger<UpdateOrderCommandHandler> logger
    ) : IRequestHandler<UpdateOrderCommand, BaseResult<OrderViewModel>>
{
    public async Task<BaseResult<OrderViewModel>> Handle(
        UpdateOrderCommand request, 
        CancellationToken cancellationToken)
    {
        var order = request.ToDomain();

        var orderDb = await orderRepository.GetByIdAsync(order.Id);

        if (orderDb == null)
        {
            logger.LogInformation("Order not found");
            throw new NotFoundException("Order not found");
        }

        var user = await applicationUserService.FindByIdAsync(request.IdUser);
        if (user == null)
        {
            logger.LogInformation("User not found");
            throw new NotFoundException("User not found");
        }

        var validator = new OrderValidation();
        var result = validator.Validate(order);

        if (!result.IsValid)
        {
            throw new ValidationException(result.Errors);
        }

        orderDb.Description = order.Description;
        orderDb.Value = order.Value;
        orderDb.DateOrder = order.DateOrder;
        orderDb.IdUser = order.IdUser;
        orderDb.User = null;

        await orderRepository.UpdateAsync(orderDb);

        return new BaseResult<OrderViewModel>(new OrderViewModel(orderDb), true, "Order updated successfully");

    }
}
