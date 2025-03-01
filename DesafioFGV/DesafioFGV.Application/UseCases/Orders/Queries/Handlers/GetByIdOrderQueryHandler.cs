using DesafioFGV.Application.UseCases.Orders.ViewModels;
using DesafioFGV.Domain.Exceptions;
using DesafioFGV.Domain.Repositories;
using DesafioFGV.Domain.Shared.Responses;
using MediatR;

namespace DesafioFGV.Application.UseCases.Orders.Queries.Handlers;

public class GetByIdOrderQueryHandler(
    IOrderRepository orderRepository
    ) : 
    IRequestHandler<GetByIdOrderQuery, BaseResult<OrderViewModel>>
{
    public async Task<BaseResult<OrderViewModel>> Handle(GetByIdOrderQuery request, CancellationToken cancellationToken)
    {
       var order = await orderRepository.GetByIdAsync(request.Id);

        if (order == null)
            throw new NotFoundException("Not found");

        return new BaseResult<OrderViewModel>(new OrderViewModel(order));
    }
}
