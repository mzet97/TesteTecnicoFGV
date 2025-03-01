using DesafioFGV.Application.UseCases.Auth.ViewModels;
using DesafioFGV.Domain.Shared.Responses;
using MediatR;

namespace DesafioFGV.Application.UseCases.Auth.Commands;

public class LoginUserCommand : IRequest<BaseResult<LoginResponseViewModel>>
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
