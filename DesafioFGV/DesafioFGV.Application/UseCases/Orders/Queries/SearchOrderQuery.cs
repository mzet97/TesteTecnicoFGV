using DesafioFGV.Application.UseCases.Orders.ViewModels;
using DesafioFGV.Application.ViewModels;
using DesafioFGV.Domain.Shared.Responses;
using MediatR;

namespace DesafioFGV.Application.UseCases.Orders.Queries;

public class SearchOrderQuery : BaseSearch, IRequest<BaseResultList<OrderViewModel>>
{
    public string Description { get; set; }

    public decimal Value { get; set; }

    public DateTime DateOrder { get; set; }

    public Guid IdUser { get; set; }

    public string UserName { get; set; }
    public string Email { get; set; }

    public bool Include { get; set; }
}
