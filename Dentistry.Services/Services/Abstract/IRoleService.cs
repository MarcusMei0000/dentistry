using Dentistry.Services.Models;

namespace Dentistry.Services.Abstract;

public interface IRoleService
{
    RoleModel GetRole(Guid id);

    RoleModel UpdateRole(Guid id, UpdateRoleModel role);
    RoleModel AddRole(RoleModel roleModel);

    void DeleteRole(Guid id);

    PageModel<RoleModel> GetRoles(int limit = 20, int offset = 0);
}