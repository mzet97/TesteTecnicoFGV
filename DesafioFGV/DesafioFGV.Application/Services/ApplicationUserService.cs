using DesafioFGV.Domain.Identities;
using DesafioFGV.Domain.Services.Interface;
using DesafioFGV.Domain.Shared;
using DesafioFGV.Domain.Shared.Responses;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DesafioFGV.Application.Services;

public class ApplicationUserService : IApplicationUserService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public ApplicationUserService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
    }

    public async Task<IdentityResult> CreateAsync(ApplicationUser user, string password)
    {
        if (user == null) throw new ArgumentNullException(nameof(user));
        return await _userManager.CreateAsync(user, password);
    }

    public async Task<ApplicationUser?> FindByIdAsync(Guid userId)
    {
        return await _userManager.FindByIdAsync(userId.ToString());
    }

    public async Task<ApplicationUser?> FindByEmailAsync(string email)
    {
        if (string.IsNullOrEmpty(email)) throw new ArgumentException("Email cannot be null or empty", nameof(email));
        return await _userManager.FindByEmailAsync(email);
    }

    public async Task<ApplicationUser?> FindByNameAsync(string name)
    {
        if (string.IsNullOrEmpty(name)) throw new ArgumentException("Name cannot be null or empty", nameof(name));
        return await _userManager.FindByNameAsync(name);
    }

    public async Task<IEnumerable<ApplicationUser>> GetAllUsersAsync()
    {
        return await _userManager.Users.ToListAsync();
    }

    public async Task<BaseResultList<ApplicationUser>> SearchAsync(
         Expression<Func<ApplicationUser, bool>>? predicate = null,
         Func<IQueryable<ApplicationUser>, IOrderedQueryable<ApplicationUser>>? orderBy = null,
          int pageSize = 10, int page = 1
         )
    {
        if (pageSize <= 0) throw new ArgumentOutOfRangeException(nameof(pageSize));
        if (page <= 0) throw new ArgumentOutOfRangeException(nameof(page));

        var query = _userManager.Users.AsQueryable();

        if (predicate != null)
        {
            query = query.Where(predicate);
        }

        var totalCount = await query.CountAsync();
        var paged = PagedResult.Create(page, pageSize, totalCount);

        if (orderBy != null)
        {
            query = orderBy(query);
        }

        var data = await query.Skip(paged.Skip()).Take(pageSize).ToListAsync();
        return new BaseResultList<ApplicationUser>(data, paged);
    }

    public async Task<IdentityResult> UpdateAsync(ApplicationUser user)
    {
        if (user == null) throw new ArgumentNullException(nameof(user));
        return await _userManager.UpdateAsync(user);
    }

    public async Task<IdentityResult> DeleteAsync(Guid userId)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user == null)
        {
            return IdentityResult.Failed(new IdentityError { Description = "User not found" });
        }
        return await _userManager.DeleteAsync(user);
    }

    public async Task<bool> CheckPasswordAsync(ApplicationUser user, string password)
    {
        if (user == null) throw new ArgumentNullException(nameof(user));
        return await _userManager.CheckPasswordAsync(user, password);
    }

    // Claims
    public async Task<IList<ApplicationUserClaim>> GetClaimsAsync(ApplicationUser user)
    {
        var claims = await _userManager.GetClaimsAsync(user);
        return claims.Select(c => new ApplicationUserClaim { ClaimType = c.Type, ClaimValue = c.Value, UserId = user.Id }).ToList();
    }

    public async Task<IdentityResult> AddClaimToUserAsync(ApplicationUser user, string claimType, string claimValue)
    {
        return await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim(claimType, claimValue));
    }

    public async Task<IdentityResult> RemoveClaimFromUserAsync(ApplicationUser user, string claimType)
    {
        var claims = await _userManager.GetClaimsAsync(user);
        var claimToRemove = claims.FirstOrDefault(c => c.Type == claimType);
        if (claimToRemove != null)
        {
            return await _userManager.RemoveClaimAsync(user, claimToRemove);
        }
        return IdentityResult.Failed(new IdentityError { Description = "Claim not found" });
    }

    // Roles
    public async Task<IList<string>> GetRolesAsync(ApplicationUser user)
    {
        return await _userManager.GetRolesAsync(user);
    }

    public async Task<IdentityResult> AddUserToRoleAsync(ApplicationUser user, string roleName)
    {
        return await _userManager.AddToRoleAsync(user, roleName);
    }

    public async Task<IdentityResult> RemoveUserFromRoleAsync(ApplicationUser user, string roleName)
    {
        return await _userManager.RemoveFromRoleAsync(user, roleName);
    }

    // Logins
    public async Task<IList<UserLoginInfo>> GetUserLoginsAsync(ApplicationUser user)
    {
        return await _userManager.GetLoginsAsync(user);
    }

    public async Task<IdentityResult> AddLoginToUserAsync(ApplicationUser user, UserLoginInfo loginInfo)
    {
        return await _userManager.AddLoginAsync(user, loginInfo);
    }

    public async Task<IdentityResult> RemoveLoginFromUserAsync(ApplicationUser user, string loginProvider, string providerKey)
    {
        return await _userManager.RemoveLoginAsync(user, loginProvider, providerKey);
    }

    // Tokens
    public async Task<string> GenerateUserTokenAsync(ApplicationUser user, string tokenProvider, string purpose)
    {
        return await _userManager.GenerateUserTokenAsync(user, tokenProvider, purpose);
    }

    public async Task<bool> VerifyUserTokenAsync(ApplicationUser user, string tokenProvider, string purpose, string token)
    {
        return await _userManager.VerifyUserTokenAsync(user, tokenProvider, purpose, token);
    }

    // SignInManager Methods
    public async Task<SignInResult> PasswordSignInAsync(string userName, string password, bool isPersistent, bool lockoutOnFailure)
    {
        return await _signInManager.PasswordSignInAsync(userName, password, isPersistent, lockoutOnFailure);
    }

    public async Task<SignInResult> PasswordSignInByEmailAsync(string email, string password, bool isPersistent, bool lockoutOnFailure)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
        {
            return SignInResult.Failed;
        }

        return await _signInManager.PasswordSignInAsync(user, password, isPersistent, lockoutOnFailure);
    }


    public async Task TrySignInAsync(ApplicationUser user)
    {
        await _signInManager.SignInAsync(user, false);
    }

    public async Task<SignInResult> CheckPasswordSignInAsync(ApplicationUser user, string password, bool lockoutOnFailure)
    {
        return await _signInManager.CheckPasswordSignInAsync(user, password, lockoutOnFailure);
    }

    public async Task RefreshSignInAsync(ApplicationUser user)
    {
        await _signInManager.RefreshSignInAsync(user);
    }

    public async Task<bool> AddPasswordAsync(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email);

        if (user != null)
        {
            var result = await _userManager.AddPasswordAsync(user, password);

            if (result.Succeeded)
                return true;
        }

        return false;
    }

    public async Task<IdentityResult> AddPasswordAsync(ApplicationUser user, string password)
    {
        return await _userManager.AddPasswordAsync(user, password);
    }

    // Email Confirmation
    public async Task<string> GenerateEmailConfirmationTokenAsync(ApplicationUser user)
    {
        if (user == null) throw new ArgumentNullException(nameof(user));
        return await _userManager.GenerateEmailConfirmationTokenAsync(user);
    }

    public async Task<IdentityResult> ConfirmEmailAsync(ApplicationUser user, string token)
    {
        if (user == null) throw new ArgumentNullException(nameof(user));
        return await _userManager.ConfirmEmailAsync(user, token);
    }

    public async Task<IdentityResult> CreateAsync(ApplicationUser user)
    {
        if (user == null) throw new ArgumentNullException(nameof(user));
        return await _userManager.CreateAsync(user);
    }
}
