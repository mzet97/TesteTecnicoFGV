using DesafioFGV.Application.UseCases.Orders.Commands;
using DesafioFGV.Domain.Exceptions;
using DesafioFGV.Domain.Repositories;
using DesafioFGV.Domain.Services.Interface;
using DesafioFGV.Domain.Shared.Responses;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DesafioFGV.Application.UseCases.Users.Commands.Handlers;

public class DeleteUserCommandHandler(
    IApplicationUserService applicationUserService,
    IOrderRepository orderRepository,
    IMediator mediator,
    ILogger<DeleteUserCommandHandler> logger) :
    IRequestHandler<DeleteUserCommand, BaseResult>
{
    public async Task<BaseResult> Handle(
        DeleteUserCommand request, 
        CancellationToken cancellationToken)
    {
        var user = await applicationUserService.FindByIdAsync(request.Id);

        if (user == null)
        {
            logger.LogWarning("User not found");
            throw new NotFoundException($"User not found");
        }

        var orders = await orderRepository.FindAsync(
            x => x.IdUser == request.Id);


        foreach (var order in orders)
        {
            await orderRepository.RemoveAsync(order.Id);
        }

        var result = await applicationUserService.DeleteAsync(request.Id);

        if(result.Succeeded)
        {
            logger.LogInformation("User has been deleted");
            return new BaseResult(true, "User has been deleted");
        }

        logger.LogError("Error delete user");
        return new BaseResult(false, "Error delete user");
    }
}
