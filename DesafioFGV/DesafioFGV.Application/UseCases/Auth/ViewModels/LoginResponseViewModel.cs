using DesafioFGV.Domain.Shared.Responses;
using MediatR;

namespace DesafioFGV.Application.UseCases.Auth.ViewModels;

public class LoginResponseViewModel : IRequest<BaseResult<LoginResponseViewModel>>
{
    public string AccessToken { get; set; } = string.Empty;
    public double ExpiresIn { get; set; }
    public UserTokenViewModel UserToken { get; set; } = new UserTokenViewModel();
}
