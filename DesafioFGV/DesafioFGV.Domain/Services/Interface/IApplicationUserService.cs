using DesafioFGV.Domain.Identities;
using DesafioFGV.Domain.Shared.Responses;
using Microsoft.AspNetCore.Identity;
using System.Linq.Expressions;

namespace DesafioFGV.Domain.Services.Interface;

public interface IApplicationUserService
{
    Task<IdentityResult> CreateAsync(ApplicationUser user, string password);
    Task<IdentityResult> CreateAsync(ApplicationUser user);
    Task<ApplicationUser?> FindByIdAsync(Guid userId);
    Task<ApplicationUser?> FindByEmailAsync(string email);
    Task<ApplicationUser?> FindByNameAsync(string name);
    Task<IEnumerable<ApplicationUser>> GetAllUsersAsync();
    Task<BaseResultList<ApplicationUser>> SearchAsync(
         Expression<Func<ApplicationUser, bool>>? predicate = null,
         Func<IQueryable<ApplicationUser>, IOrderedQueryable<ApplicationUser>>? orderBy = null,
          int pageSize = 10, int page = 1
         );
    Task<IdentityResult> UpdateAsync(ApplicationUser user);
    Task<IdentityResult> DeleteAsync(Guid userId);
    Task<bool> CheckPasswordAsync(ApplicationUser user, string password);

    // Claims
    Task<IList<ApplicationUserClaim>> GetClaimsAsync(ApplicationUser user);
    Task<IdentityResult> AddClaimToUserAsync(ApplicationUser user, string claimType, string claimValue);
    Task<IdentityResult> RemoveClaimFromUserAsync(ApplicationUser user, string claimType);

    // Roles
    Task<IList<string>> GetRolesAsync(ApplicationUser user);
    Task<IdentityResult> AddUserToRoleAsync(ApplicationUser user, string roleName);
    Task<IdentityResult> RemoveUserFromRoleAsync(ApplicationUser user, string roleName);

    // Logins
    Task<IList<UserLoginInfo>> GetUserLoginsAsync(ApplicationUser user);
    Task<IdentityResult> AddLoginToUserAsync(ApplicationUser user, UserLoginInfo loginInfo);
    Task<IdentityResult> RemoveLoginFromUserAsync(ApplicationUser user, string loginProvider, string providerKey);

    // Tokens
    Task<string> GenerateUserTokenAsync(ApplicationUser user, string tokenProvider, string purpose);
    Task<bool> VerifyUserTokenAsync(ApplicationUser user, string tokenProvider, string purpose, string token);

    Task<SignInResult> PasswordSignInAsync(string userName, string password, bool isPersistent, bool lockoutOnFailure);

    // SignInManager Methods
    Task<SignInResult> PasswordSignInByEmailAsync(string email, string password, bool isPersistent, bool lockoutOnFailure);
    Task<SignInResult> CheckPasswordSignInAsync(ApplicationUser user, string password, bool lockoutOnFailure);
    Task RefreshSignInAsync(ApplicationUser user);

    Task TrySignInAsync(ApplicationUser user);

    Task<bool> AddPasswordAsync(string email, string password);
    Task<IdentityResult> AddPasswordAsync(ApplicationUser user, string password);

    // Email Confirmation
    Task<string> GenerateEmailConfirmationTokenAsync(ApplicationUser user);
    Task<IdentityResult> ConfirmEmailAsync(ApplicationUser user, string token);
}