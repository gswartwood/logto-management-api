using Logto.ManagementApi.Applications;

namespace Logto.ManagementApi.Roles;

public interface IRolesClient
{
    Task<IReadOnlyList<RoleWithStats>> ListAsync(ListRolesRequest? request = null, CancellationToken cancellationToken = default);
    Task<Role> CreateAsync(CreateRoleRequest request, CancellationToken cancellationToken = default);
    Task<Role> GetAsync(string id, CancellationToken cancellationToken = default);
    Task<Role> UpdateAsync(string id, UpdateRoleRequest request, CancellationToken cancellationToken = default);
    Task DeleteAsync(string id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Application>> ListApplicationsAsync(string roleId, int? page = null, int? pageSize = null, CancellationToken cancellationToken = default);
    Task AssignApplicationsAsync(string roleId, AssignRoleApplicationsRequest request, CancellationToken cancellationToken = default);
    Task DeleteApplicationAsync(string roleId, string applicationId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<RoleScope>> ListScopesAsync(string roleId, int? page = null, int? pageSize = null, CancellationToken cancellationToken = default);
    Task AssignScopesAsync(string roleId, AssignRoleScopesRequest request, CancellationToken cancellationToken = default);
    Task DeleteScopeAsync(string roleId, string scopeId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<RoleUser>> ListUsersAsync(string roleId, int? page = null, int? pageSize = null, CancellationToken cancellationToken = default);
    Task AssignUsersAsync(string roleId, AssignRoleUsersRequest request, CancellationToken cancellationToken = default);
    Task DeleteUserAsync(string roleId, string userId, CancellationToken cancellationToken = default);
}
