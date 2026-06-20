using System.Net.Http.Json;
using Logto.ManagementApi.Utils;

namespace Logto.ManagementApi.Connectors;

public sealed class ConnectorFactoriesClient(HttpClient httpClient) : IConnectorFactoriesClient
{
    public async Task<IEnumerable<ConnectorFactory>> ListAsync(CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync("connector-factories", cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<IEnumerable<ConnectorFactory>>(cancellationToken: cancellationToken) ?? [];
    }

    public async Task<ConnectorFactory> GetAsync(string id, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync($"connector-factories/{Uri.EscapeDataString(id)}", cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<ConnectorFactory>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for connector factory get.");
    }
}
