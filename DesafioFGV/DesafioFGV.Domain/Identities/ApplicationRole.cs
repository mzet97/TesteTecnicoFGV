﻿using Microsoft.AspNetCore.Identity;

namespace DesafioFGV.Domain.Identities;

public class ApplicationRole : IdentityRole<Guid>
{
    public ICollection<ApplicationUserRole> UserRoles { get; set; } = null!;
    public ICollection<ApplicationRoleClaim> RoleClaims { get; set; } = null!;

    public static ApplicationRole Create(string name)
    {
        return new ApplicationRole
        {
            Name = name
        };
    }
}
