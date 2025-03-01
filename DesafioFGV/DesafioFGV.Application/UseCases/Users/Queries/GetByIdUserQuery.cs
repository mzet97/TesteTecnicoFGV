using DesafioFGV.Application.UseCases.Users.ViewModels;
using DesafioFGV.Domain.Shared.Responses;
using MediatR;

namespace DesafioFGV.Application.UseCases.Users.Queries;

public class GetByIdUserQuery : IRequest<BaseResult<UserViewModel>>
{
    public Guid Id { get; set; }

    public GetByIdUserQuery(Guid id)
    {
        Id = id;
    }
}
