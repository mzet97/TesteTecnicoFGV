using DesafioFGV.Application.UseCases.Users.ViewModels;
using DesafioFGV.Domain.Services.Interface;
using DesafioFGV.Domain.Shared.Responses;
using MediatR;

namespace DesafioFGV.Application.UseCases.Users.Queries.Handlers;

public class GetByIdUserQueryHandler(
    IApplicationUserService applicationUserService) :
    IRequestHandler<GetByIdUserQuery, BaseResult<UserViewModel>>
{
    public async Task<BaseResult<UserViewModel>> Handle(GetByIdUserQuery request, CancellationToken cancellationToken)
    {
        var users = await applicationUserService.FindByIdAsync(request.Id);

        return new BaseResult<UserViewModel>(
            new UserViewModel(users),
            true,
            "Obtido com sucesso");
    }
}

