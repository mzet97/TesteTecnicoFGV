using Microsoft.AspNetCore.Identity;

namespace DesafioFGV.Domain.Identities;

public class ApplicationUser : IdentityUser<Guid>
{
    public DateTime CreatedAt { get; set; }

    public ICollection<ApplicationUserClaim> Claims { get; set; } = null!;
    public ICollection<ApplicationUserRole> UserRoles { get; set; } = null!;
    public ICollection<ApplicationUserLogin> Logins { get; set; } = null!;
    public ICollection<ApplicationUserToken> Tokens { get; set; } = null!;
}
