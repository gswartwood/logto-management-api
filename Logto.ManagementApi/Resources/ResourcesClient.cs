using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Logto.ManagementApi.Utils;

namespace Logto.ManagementApi.Resources;

public sealed class ResourcesClient(HttpClient httpClient) : IResourcesClient
{
    private static readonly JsonSerializerOptions WriteOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
    };

    public async Task<IReadOnlyList<Resource>> ListAsync(bool? includeScopes = null, int? page = null, int? pageSize = null, CancellationToken cancellationToken = default)
    {
        var query = new StringBuilder();
        if (includeScopes is true) query.Append("includeScopes=true&");
        if (page is not null) query.Append($"page={page}&");
        if (pageSize is not null) query.Append($"page_size={pageSize}&");
        var url = query.Length > 0 ? $"resources?{query.ToString().TrimEnd('&')}" : "resources";
        var response = await httpClient.GetAsync(url, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<Resource[]>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for resources list.");
    }

    public async Task<Resource> CreateAsync(CreateResourceRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync("resources", request, WriteOptions, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<Resource>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for resources create.");
    }

    public async Task<Resource> GetAsync(string id, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync($"resources/{Uri.EscapeDataString(id)}", cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<Resource>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for resources get.");
    }

    public async Task<Resource> UpdateAsync(string id, UpdateResourceRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PatchAsJsonAsync($"resources/{Uri.EscapeDataString(id)}", request, WriteOptions, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<Resource>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for resources update.");
    }

    public async Task DeleteAsync(string id, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.DeleteAsync($"resources/{Uri.EscapeDataString(id)}", cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
    }

    public async Task<Resource> UpdateIsDefaultAsync(string id, UpdateResourceIsDefaultRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PatchAsJsonAsync($"resources/{Uri.EscapeDataString(id)}/is-default", request, WriteOptions, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<Resource>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for resources update is-default.");
    }

    public async Task<IReadOnlyList<ResourceScope>> ListScopesAsync(string resourceId, int? page = null, int? pageSize = null, Dictionary<string, string>? searchParams = null, CancellationToken cancellationToken = default)
    {
        var query = new StringBuilder();
        if (page is not null) query.Append($"page={page}&");
        if (pageSize is not null) query.Append($"page_size={pageSize}&");
        if (searchParams is not null)
            foreach (var (key, value) in searchParams)
                query.Append($"{Uri.EscapeDataString(key)}={Uri.EscapeDataString(value)}&");
        var url = query.Length > 0
            ? $"resources/{Uri.EscapeDataString(resourceId)}/scopes?{query.ToString().TrimEnd('&')}"
            : $"resources/{Uri.EscapeDataString(resourceId)}/scopes";
        var response = await httpClient.GetAsync(url, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<ResourceScope[]>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for resources scopes list.");
    }

    public async Task<ResourceScope> CreateScopeAsync(string resourceId, CreateResourceScopeRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync($"resources/{Uri.EscapeDataString(resourceId)}/scopes", request, WriteOptions, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<ResourceScope>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for resources scope create.");
    }

    public async Task<ResourceScope> UpdateScopeAsync(string resourceId, string scopeId, UpdateResourceScopeRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PatchAsJsonAsync($"resources/{Uri.EscapeDataString(resourceId)}/scopes/{Uri.EscapeDataString(scopeId)}", request, WriteOptions, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<ResourceScope>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for resources scope update.");
    }

    public async Task DeleteScopeAsync(string resourceId, string scopeId, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.DeleteAsync($"resources/{Uri.EscapeDataString(resourceId)}/scopes/{Uri.EscapeDataString(scopeId)}", cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
    }
}
