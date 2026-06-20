namespace Logto.ManagementApi.Organizations;

public interface IOrganizationScopesClient
{
    Task<IReadOnlyList<OrganizationScope>> ListAsync(string? q = null, int? page = null, int? pageSize = null, CancellationToken cancellationToken = default);
    Task<OrganizationScope> CreateAsync(CreateOrganizationScopeRequest request, CancellationToken cancellationToken = default);
    Task<OrganizationScope> GetAsync(string id, CancellationToken cancellationToken = default);
    Task<OrganizationScope> UpdateAsync(string id, UpdateOrganizationScopeRequest request, CancellationToken cancellationToken = default);
    Task DeleteAsync(string id, CancellationToken cancellationToken = default);
}
