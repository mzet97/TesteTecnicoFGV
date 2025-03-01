using DesafioFGV.Domain.Exceptions;
using DesafioFGV.Domain.Services.Interface;
using DesafioFGV.Domain.Shared.Responses;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DesafioFGV.Application.UseCases.Users.Commands.Handlers;

public class UpdatePasswordCommandHandler(
    IApplicationUserService applicationUserService,
    ILogger<UpdatePasswordCommandHandler> logger) :
    IRequestHandler<UpdatePasswordCommand, BaseResult>
{
    public async Task<BaseResult> Handle(
        UpdatePasswordCommand request, 
        CancellationToken cancellationToken)
    {
        var user = await applicationUserService.FindByIdAsync(request.Id);
        if (user == null)
        {
            logger.LogInformation("User not found");
            throw new NotFoundException($"User not found");
        }

        if (request.NewPassword != request.ConfirmPassword)
        {
            logger.LogInformation("Passwords do not match");
            return new BaseResult(false, "Passwords do not match");
        }

        var resultUpdatePassword = await applicationUserService.AddPasswordAsync(user, request.NewPassword);

        if (resultUpdatePassword.Succeeded)
        {
            logger.LogInformation("Password updated successfully");
            return new BaseResult(true, "Password updated successfully");
        }

        logger.LogInformation("Error updating password");

        return new BaseResult(false, "Error updating password");
    }
}
