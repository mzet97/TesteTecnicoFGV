using DesafioFGV.Application.UseCases.Users.ViewModels;
using DesafioFGV.Domain.Identities;
using DesafioFGV.Domain.Services.Interface;
using DesafioFGV.Domain.Shared.Responses;
using LinqKit;
using MediatR;
using System.Linq.Expressions;

namespace DesafioFGV.Application.UseCases.Users.Queries.Handlers;

public class SearchUserQueryHandler(
    IApplicationUserService applicationUserService) :
    IRequestHandler<SearchUserQuery, BaseResultList<UserViewModel>>
{
    public async Task<BaseResultList<UserViewModel>> Handle(SearchUserQuery request, CancellationToken cancellationToken)
    {
        Expression<Func<ApplicationUser, bool>>? filter = PredicateBuilder.New<ApplicationUser>(true);
        Func<IQueryable<ApplicationUser>, IOrderedQueryable<ApplicationUser>>? ordeBy = null;

        if (!string.IsNullOrWhiteSpace(request.Name))
        {
            filter = filter.And(x => x.UserName == request.Name);
        }

        if (!string.IsNullOrWhiteSpace(request.Email))
        {
            filter = filter.And(x => x.Email == request.Email);
        }

        if (request.Id != Guid.Empty)
        {
            filter = filter.And(x => x.Id == request.Id);
        }

        if (request.CreatedAt != default)
        {
            filter = filter.And(x => x.CreatedAt == request.CreatedAt);
        }

        if (!string.IsNullOrWhiteSpace(request.Order))
        {
            switch (request.Order)
            {
                case "Id":
                    ordeBy = x => x.OrderBy(n => n.Id);
                    break;

                case "Name":
                    ordeBy = x => x.OrderBy(n => n.UserName);
                    break;

                case "Email":
                    ordeBy = x => x.OrderBy(n => n.Email);
                    break;

                case "CreatedAt":
                    ordeBy = x => x.OrderBy(n => n.CreatedAt);
                    break;

                default:
                    ordeBy = x => x.OrderBy(n => n.Id);
                    break;
            }
        }

        var result = await applicationUserService.SearchAsync(
              filter,
              ordeBy,
              request.PageSize,
              request.PageIndex);


        return new BaseResultList<UserViewModel>(
            result.Data.Select(x => new UserViewModel(x)).ToList(), 
            result.PagedResult);
    }
}
