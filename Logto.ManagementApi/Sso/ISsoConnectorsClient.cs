namespace Logto.ManagementApi.Sso;

public interface ISsoConnectorsClient
{
    Task<IReadOnlyList<SsoConnector>> ListAsync(int? page = null, int? pageSize = null, CancellationToken cancellationToken = default);
    Task<SsoConnector> CreateAsync(CreateSsoConnectorRequest request, CancellationToken cancellationToken = default);
    Task<SsoConnector> GetAsync(string id, CancellationToken cancellationToken = default);
    Task DeleteAsync(string id, CancellationToken cancellationToken = default);
    Task<SsoConnector> UpdateAsync(string id, UpdateSsoConnectorRequest request, CancellationToken cancellationToken = default);
}
