using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using Logto.ManagementApi.Utils;

namespace Logto.ManagementApi.Connectors;

public sealed class ConnectorsClient(HttpClient httpClient) : IConnectorsClient
{
    private static readonly JsonSerializerOptions WriteOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
    };

    public async Task<IEnumerable<Connector>> ListAsync(string? target = null, CancellationToken cancellationToken = default)
    {
        var url = target is null ? "connectors" : $"connectors?target={Uri.EscapeDataString(target)}";
        var response = await httpClient.GetAsync(url, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<IEnumerable<Connector>>(cancellationToken: cancellationToken) ?? [];
    }

    public async Task<Connector> GetAsync(string id, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync($"connectors/{Uri.EscapeDataString(id)}", cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<Connector>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for connector get.");
    }

    public async Task<Connector> CreateAsync(CreateConnectorRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync("connectors", request, WriteOptions, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<Connector>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for connector create.");
    }

    public async Task<Connector> UpdateAsync(string id, UpdateConnectorRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PatchAsJsonAsync($"connectors/{Uri.EscapeDataString(id)}", request, WriteOptions, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<Connector>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for connector update.");
    }

    public async Task DeleteAsync(string id, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.DeleteAsync($"connectors/{Uri.EscapeDataString(id)}", cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
    }

    public async Task TestAsync(string factoryId, TestConnectorRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync($"connectors/{Uri.EscapeDataString(factoryId)}/test", request, WriteOptions, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
    }

    public async Task<ConnectorAuthorizationUri> GetAuthorizationUriAsync(string connectorId, GetAuthorizationUriRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync($"connectors/{Uri.EscapeDataString(connectorId)}/authorization-uri", request, WriteOptions, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<ConnectorAuthorizationUri>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for connector authorization URI.");
    }
}
