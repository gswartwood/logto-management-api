namespace Logto.ManagementApi.Connectors;

public interface IConnectorFactoriesClient
{
    Task<IEnumerable<ConnectorFactory>> ListAsync(CancellationToken cancellationToken = default);
    Task<ConnectorFactory> GetAsync(string id, CancellationToken cancellationToken = default);
}
