using DesafioFGV.Application.UseCases.Users.ViewModels;
using DesafioFGV.Domain.Exceptions;
using DesafioFGV.Domain.Services.Interface;
using DesafioFGV.Domain.Shared.Responses;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Text;

namespace DesafioFGV.Application.UseCases.Users.Commands.Handlers;

public class UpdateUserCommandHandler(
    IApplicationUserService applicationUserService, 
    ILogger<UpdateUserCommandHandler> logger) :
    IRequestHandler<UpdateUserCommand, BaseResult<UserViewModel>>
{
    public async Task<BaseResult<UserViewModel>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var userRquest = request.ToDomain();

        var user = await applicationUserService.FindByIdAsync(userRquest.Id);
        if (user == null)
        {
            logger.LogInformation("User not found");
            throw new NotFoundException($"User not found");
        }
        
        var userByEmail = await applicationUserService.FindByEmailAsync(userRquest.Email);
        if (userByEmail != null && userByEmail.Id != user.Id)
        {
            logger.LogInformation("Email already exists");
            return new BaseResult<UserViewModel>(null, false, "Email already exists");
        }

        var userByName = await applicationUserService.FindByNameAsync(userRquest.UserName);
        if (userByName != null && userByName.Id != user.Id)
        {
            logger.LogInformation("User name already exists");
            return new BaseResult<UserViewModel>(null, false, "User name already exists");
        }

        user.UserName = userRquest.UserName;
        user.Email = userRquest.Email;

        var resultUpdateUser = await applicationUserService.UpdateAsync(user);

        if (resultUpdateUser.Succeeded)
        {
            var userDb = await applicationUserService.FindByIdAsync(user.Id);
            return new BaseResult<UserViewModel>(new UserViewModel(userDb), true, "User updated successfully");
        }

        var sb = new StringBuilder();
        foreach (var error in resultUpdateUser.Errors)
        {
            sb.Append(error.Description);
        }

        logger.LogInformation($"Falha: {sb.ToString()}");
        return new BaseResult<UserViewModel>(null, false, sb.ToString());
    }
}
