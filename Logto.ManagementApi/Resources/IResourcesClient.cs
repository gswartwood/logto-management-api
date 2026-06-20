namespace Logto.ManagementApi.Resources;

public interface IResourcesClient
{
    Task<IReadOnlyList<Resource>> ListAsync(bool? includeScopes = null, int? page = null, int? pageSize = null, CancellationToken cancellationToken = default);
    Task<Resource> CreateAsync(CreateResourceRequest request, CancellationToken cancellationToken = default);
    Task<Resource> GetAsync(string id, CancellationToken cancellationToken = default);
    Task<Resource> UpdateAsync(string id, UpdateResourceRequest request, CancellationToken cancellationToken = default);
    Task DeleteAsync(string id, CancellationToken cancellationToken = default);
    Task<Resource> UpdateIsDefaultAsync(string id, UpdateResourceIsDefaultRequest request, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<ResourceScope>> ListScopesAsync(string resourceId, int? page = null, int? pageSize = null, Dictionary<string, string>? searchParams = null, CancellationToken cancellationToken = default);
    Task<ResourceScope> CreateScopeAsync(string resourceId, CreateResourceScopeRequest request, CancellationToken cancellationToken = default);
    Task<ResourceScope> UpdateScopeAsync(string resourceId, string scopeId, UpdateResourceScopeRequest request, CancellationToken cancellationToken = default);
    Task DeleteScopeAsync(string resourceId, string scopeId, CancellationToken cancellationToken = default);
}
