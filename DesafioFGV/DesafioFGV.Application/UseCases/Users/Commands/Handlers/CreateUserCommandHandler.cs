using DesafioFGV.Domain.Services.Interface;
using DesafioFGV.Domain.Shared.Responses;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Text;

namespace DesafioFGV.Application.UseCases.Users.Commands.Handlers;

public class CreateUserCommandHandler(
    IApplicationUserService applicationUserService,
    ILogger<CreateUserCommandHandler> logger) 
    : IRequestHandler<CreateUserCommand, BaseResult<Guid>>
{
    public async Task<BaseResult<Guid>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = request.ToDomain();

        var resultCreateUser = await applicationUserService.CreateAsync(user);

        if (resultCreateUser.Succeeded)
        {
            var userDb = await applicationUserService.FindByEmailAsync(request.Email);
            return new BaseResult<Guid>(userDb.Id, true, "User created successfully");
        }

        var sb = new StringBuilder();
        foreach (var error in resultCreateUser.Errors)
        {
            sb.Append(error.Description);
        }

        logger.LogInformation($"Error: {sb.ToString()}");

        return new BaseResult<Guid>(Guid.Empty, false, sb.ToString());
    }
}
