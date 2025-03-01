using DesafioFGV.Application.UseCases.Auth.ViewModels;
using DesafioFGV.Domain.Services.Interface;
using DesafioFGV.Domain.Shared.Responses;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Text;

namespace DesafioFGV.Application.UseCases.Auth.Commands.Handlers;

public class RegisterUserCommandHandler(
    IApplicationUserService applicationUserService,
    ILogger<RegisterUserCommandHandler> logger,
    IMediator mediator
    ) : IRequestHandler<RegisterUserCommand, BaseResult<LoginResponseViewModel>>
{
    public async Task<BaseResult<LoginResponseViewModel>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var user = request.ToDomain();

        var resultCreateUser = await applicationUserService.CreateAsync(user, request.Password);

        if (resultCreateUser.Succeeded)
        {
            await applicationUserService.TrySignInAsync(user);
            return await mediator.Send(new GetTokenCommand { Email = request.Email });
        }

        var sb = new StringBuilder();
        foreach (var error in resultCreateUser.Errors)
        {
            sb.Append(error.Description);
        }

        logger.LogInformation($"Falha: {sb.ToString()}");

        return new BaseResult<LoginResponseViewModel>(null, false, sb.ToString());
    }
}
