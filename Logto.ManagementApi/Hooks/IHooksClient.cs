namespace Logto.ManagementApi.Hooks;

public interface IHooksClient
{
    Task<IReadOnlyList<Hook>> ListAsync(bool? includeExecutionStats = null, int? page = null, int? pageSize = null, CancellationToken cancellationToken = default);
    Task<Hook> CreateAsync(CreateHookRequest request, CancellationToken cancellationToken = default);
    Task<Hook> GetAsync(string id, bool? includeExecutionStats = null, CancellationToken cancellationToken = default);
    Task<Hook> UpdateAsync(string id, UpdateHookRequest request, CancellationToken cancellationToken = default);
    Task DeleteAsync(string id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<HookRecentLog>> ListRecentLogsAsync(string id, ListHookRecentLogsRequest? request = null, CancellationToken cancellationToken = default);
    Task TestAsync(string id, TestHookRequest request, CancellationToken cancellationToken = default);
    Task<Hook> UpdateSigningKeyAsync(string id, CancellationToken cancellationToken = default);
}
