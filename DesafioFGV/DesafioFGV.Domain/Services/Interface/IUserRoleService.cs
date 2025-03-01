﻿using DesafioFGV.Domain.Identities;

namespace DesafioFGV.Domain.Services.Interface;

public interface IUserRoleService
{
    Task AddUserToRoleAsync(ApplicationUser user, string roleName);

    Task RemoveUserFromRoleAsync(ApplicationUser user, string roleName);

    Task<IList<string>> GetUserRolesAsync(ApplicationUser user);

    Task<IList<ApplicationUser>> GetUsersInRoleAsync(string roleName);
}
