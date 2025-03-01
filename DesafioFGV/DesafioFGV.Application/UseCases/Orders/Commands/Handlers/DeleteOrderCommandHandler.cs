using DesafioFGV.Domain.Exceptions;
using DesafioFGV.Domain.Repositories;
using DesafioFGV.Domain.Shared.Responses;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DesafioFGV.Application.UseCases.Orders.Commands.Handlers;

public class DeleteOrderCommandHandler(
    IOrderRepository orderRepository,
    ILogger<DeleteOrderCommandHandler> logger
    ) : IRequestHandler<DeleteOrderCommand, BaseResult>
{
    public async Task<BaseResult> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await orderRepository.GetByIdNoIncludeAsync(request.Id);

        if (order == null)
        {
            logger.LogWarning($"Order {request.Id} not found.");
            throw new NotFoundException($"Order {request.Id} not found.");
        }
        
        await orderRepository.RemoveAsync(order.Id);
        logger.LogInformation($"Order {order.Id} deleted successfully.");

        return new BaseResult(true, "Order deleted successfully.");
    }
}
