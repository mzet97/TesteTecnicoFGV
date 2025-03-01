using DesafioFGV.Application.UseCases.Orders.ViewModels;
using DesafioFGV.Domain.Entities;
using DesafioFGV.Domain.Repositories;
using DesafioFGV.Domain.Shared.Responses;
using LinqKit;
using MediatR;
using System.Linq.Expressions;

namespace DesafioFGV.Application.UseCases.Orders.Queries.Handlers;

public class SearchOrderQueryHandler(
    IOrderRepository orderRepository
    ) : IRequestHandler<SearchOrderQuery, BaseResultList<OrderViewModel>>
{
    public async Task<BaseResultList<OrderViewModel>> Handle(
        SearchOrderQuery request, 
        CancellationToken cancellationToken)
    {
        Expression<Func<Order, bool>>? filter = PredicateBuilder.New<Order>(true);
        Func<IQueryable<Order>, IOrderedQueryable<Order>>? ordeBy = null;

        if (!string.IsNullOrWhiteSpace(request.Description))
        {
            filter = filter.And(x => x.Description == request.Description);
        }

        if (request.Value > 0)
        {
            filter = filter.And(x => x.Value == request.Value);
        }

        if (request.Id != Guid.Empty)
        {
            filter = filter.And(x => x.Id == request.Id);
        }

        if (request.IdUser != Guid.Empty)
        {
            filter = filter.And(x => x.IdUser == request.IdUser);
        }

        if (!string.IsNullOrWhiteSpace(request.UserName))
        {
            filter = filter.And(x => x.User.UserName == request.UserName);
        }

        if (!string.IsNullOrWhiteSpace(request.Email))
        {
            filter = filter.And(x => x.User.Email == request.Email);
        }

        if (request.DateOrder != default)
        {
            filter = filter.And(x => x.DateOrder == request.DateOrder);
        }

        if (request.CreatedAt != default)
        {
            filter = filter.And(x => x.CreatedAt == request.CreatedAt);
        }

        if (request.UpdatedAt != default)
        {
            filter = filter.And(x => x.UpdatedAt == request.UpdatedAt);
        }

        if (request.DeletedAt != new DateTime())
        {
            filter = filter.And(x => x.DeletedAt == request.DeletedAt);
        }

        if (!string.IsNullOrWhiteSpace(request.Order))
        {
            switch (request.Order)
            {
                case "Id":
                    ordeBy = x => x.OrderBy(n => n.Id);
                    break;

                case "Value":
                    ordeBy = x => x.OrderBy(n => n.Value);
                    break;

                case "Description":
                    ordeBy = x => x.OrderBy(n => n.Description);
                    break;

                case "UserName":
                    ordeBy = x => x.OrderBy(n => n.User.UserName);
                    break;

                case "Email":
                    ordeBy = x => x.OrderBy(n => n.User.Email);
                    break;

                case "IdUser":
                    ordeBy = x => x.OrderBy(n => n.IdUser);
                    break;

                case "DateOrder":
                    ordeBy = x => x.OrderBy(n => n.DateOrder);
                    break;

                case "CreatedAt":
                    ordeBy = x => x.OrderBy(n => n.CreatedAt);
                    break;

                case "UpdatedAt":
                    ordeBy = x => x.OrderBy(n => n.UpdatedAt);
                    break;

                case "DeletedAt":
                    ordeBy = x => x.OrderBy(n => n.DeletedAt);
                    break;

                default:
                    ordeBy = x => x.OrderBy(n => n.Id);
                    break;
            }  
        }

        var result = await orderRepository
             .SearchAsync(
                 filter,
                 ordeBy,
                 request.Include ? "User" : "",
                 request.PageSize,
                 request.PageIndex);

        return new BaseResultList<OrderViewModel>(
            result.Data.Select(x => new OrderViewModel(x)).ToList(),
            result.PagedResult);
    }
}
