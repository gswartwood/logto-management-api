using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using Logto.ManagementApi.Utils;

namespace Logto.ManagementApi.Domains;

public sealed class DomainsClient(HttpClient httpClient) : IDomainsClient
{
    private static readonly JsonSerializerOptions WriteOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
    };

    public async Task<IReadOnlyList<DomainEntry>> ListAsync(CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync("domains", cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<DomainEntry[]>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for domains list.");
    }

    public async Task<DomainEntry> CreateAsync(CreateDomainRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync("domains", request, WriteOptions, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<DomainEntry>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for domains create.");
    }

    public async Task<DomainEntry> GetAsync(string id, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync($"domains/{Uri.EscapeDataString(id)}", cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<DomainEntry>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for domains get.");
    }

    public async Task DeleteAsync(string id, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.DeleteAsync($"domains/{Uri.EscapeDataString(id)}", cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
    }

    public async Task<CleanupDomainsResult> CleanupAsync(CleanupDomainsRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync("domains/cleanup", request, WriteOptions, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<CleanupDomainsResult>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for domains cleanup.");
    }
}
