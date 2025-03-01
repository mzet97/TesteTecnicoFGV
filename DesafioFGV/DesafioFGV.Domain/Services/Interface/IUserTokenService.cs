using DesafioFGV.Domain.Identities;

namespace DesafioFGV.Domain.Services.Interface;

public interface IUserTokenService
{
    Task AddUserTokenAsync(ApplicationUserToken userToken);

    Task<ApplicationUserToken?> GetUserTokenAsync(Guid userId, string loginProvider, string name);

    Task RemoveUserTokenAsync(ApplicationUserToken userToken);
}