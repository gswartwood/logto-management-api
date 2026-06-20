namespace Logto.ManagementApi.Organizations;

public interface IOrganizationRolesClient
{
    Task<IReadOnlyList<OrganizationRoleWithScopes>> ListAsync(string? q = null, int? page = null, int? pageSize = null, CancellationToken cancellationToken = default);
    Task<OrganizationRole> CreateAsync(CreateOrganizationRoleRequest request, CancellationToken cancellationToken = default);
    Task<OrganizationRole> GetAsync(string id, CancellationToken cancellationToken = default);
    Task<OrganizationRole> UpdateAsync(string id, UpdateOrganizationRoleRequest request, CancellationToken cancellationToken = default);
    Task DeleteAsync(string id, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<OrganizationRoleScope>> ListScopesAsync(string id, int? page = null, int? pageSize = null, CancellationToken cancellationToken = default);
    Task AssignScopesAsync(string id, OrganizationScopeIdsRequest request, CancellationToken cancellationToken = default);
    Task ReplaceScopesAsync(string id, OrganizationScopeIdsRequest request, CancellationToken cancellationToken = default);
    Task RemoveScopeAsync(string id, string organizationScopeId, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<OrganizationRoleResourceScope>> ListResourceScopesAsync(string id, int? page = null, int? pageSize = null, CancellationToken cancellationToken = default);
    Task AssignResourceScopesAsync(string id, ResourceScopeIdsRequest request, CancellationToken cancellationToken = default);
    Task ReplaceResourceScopesAsync(string id, ResourceScopeIdsRequest request, CancellationToken cancellationToken = default);
    Task RemoveResourceScopeAsync(string id, string scopeId, CancellationToken cancellationToken = default);
}
