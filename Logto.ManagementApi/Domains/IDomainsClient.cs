namespace Logto.ManagementApi.Domains;

public interface IDomainsClient
{
    Task<IReadOnlyList<DomainEntry>> ListAsync(CancellationToken cancellationToken = default);
    Task<DomainEntry> CreateAsync(CreateDomainRequest request, CancellationToken cancellationToken = default);
    Task<DomainEntry> GetAsync(string id, CancellationToken cancellationToken = default);
    Task DeleteAsync(string id, CancellationToken cancellationToken = default);
    Task<CleanupDomainsResult> CleanupAsync(CleanupDomainsRequest request, CancellationToken cancellationToken = default);
}
