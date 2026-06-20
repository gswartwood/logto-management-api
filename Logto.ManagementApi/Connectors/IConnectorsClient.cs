namespace Logto.ManagementApi.Connectors;

public interface IConnectorsClient
{
    Task<IEnumerable<Connector>> ListAsync(string? target = null, CancellationToken cancellationToken = default);
    Task<Connector> GetAsync(string id, CancellationToken cancellationToken = default);
    Task<Connector> CreateAsync(CreateConnectorRequest request, CancellationToken cancellationToken = default);
    Task<Connector> UpdateAsync(string id, UpdateConnectorRequest request, CancellationToken cancellationToken = default);
    Task DeleteAsync(string id, CancellationToken cancellationToken = default);
    Task TestAsync(string factoryId, TestConnectorRequest request, CancellationToken cancellationToken = default);
    Task<ConnectorAuthorizationUri> GetAuthorizationUriAsync(string connectorId, GetAuthorizationUriRequest request, CancellationToken cancellationToken = default);
}
