using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using Logto.ManagementApi.Utils;

namespace Logto.ManagementApi.Sso;

public sealed class SsoConnectorsClient(HttpClient httpClient) : ISsoConnectorsClient
{
    private static readonly JsonSerializerOptions WriteOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
    };

    public async Task<IReadOnlyList<SsoConnector>> ListAsync(int? page = null, int? pageSize = null, CancellationToken cancellationToken = default)
    {
        var query = new List<string>();
        if (page.HasValue) query.Add($"page={page.Value}");
        if (pageSize.HasValue) query.Add($"page_size={pageSize.Value}");
        var qs = query.Count > 0 ? "?" + string.Join("&", query) : "";
        var response = await httpClient.GetAsync($"sso-connectors{qs}", cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<SsoConnector[]>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for sso-connectors list.");
    }

    public async Task<SsoConnector> CreateAsync(CreateSsoConnectorRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync("sso-connectors", request, WriteOptions, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<SsoConnector>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for sso-connectors create.");
    }

    public async Task<SsoConnector> GetAsync(string id, CancellationToken cancellationToken = default)
    {
        var escaped = Uri.EscapeDataString(id);
        var response = await httpClient.GetAsync($"sso-connectors/{escaped}", cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<SsoConnector>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for sso-connectors get.");
    }

    public async Task DeleteAsync(string id, CancellationToken cancellationToken = default)
    {
        var escaped = Uri.EscapeDataString(id);
        var response = await httpClient.DeleteAsync($"sso-connectors/{escaped}", cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
    }

    public async Task<SsoConnector> UpdateAsync(string id, UpdateSsoConnectorRequest request, CancellationToken cancellationToken = default)
    {
        var escaped = Uri.EscapeDataString(id);
        var response = await httpClient.PatchAsJsonAsync($"sso-connectors/{escaped}", request, WriteOptions, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<SsoConnector>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for sso-connectors update.");
    }
}
