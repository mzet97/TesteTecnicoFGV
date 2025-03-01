using Microsoft.AspNetCore.Identity;

namespace DesafioFGV.Domain.Identities;

public class ApplicationUserRole : IdentityUserRole<Guid>
{
    public ApplicationUser User { get; set; } = null!;
    public ApplicationRole Role { get; set; } = null!;
}

