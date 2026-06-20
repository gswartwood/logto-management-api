using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Logto.ManagementApi.Utils;

namespace Logto.ManagementApi.Organizations;

public sealed class OrganizationScopesClient(HttpClient httpClient) : IOrganizationScopesClient
{
    private static readonly JsonSerializerOptions WriteOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
    };

    public async Task<IReadOnlyList<OrganizationScope>> ListAsync(string? q = null, int? page = null, int? pageSize = null, CancellationToken cancellationToken = default)
    {
        var query = new StringBuilder();
        if (q is not null) query.Append($"q={Uri.EscapeDataString(q)}&");
        if (page is not null) query.Append($"page={page}&");
        if (pageSize is not null) query.Append($"page_size={pageSize}&");
        var url = query.Length > 0 ? $"organization-scopes?{query.ToString().TrimEnd('&')}" : "organization-scopes";
        var response = await httpClient.GetAsync(url, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<OrganizationScope[]>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for organization-scopes list.");
    }

    public async Task<OrganizationScope> CreateAsync(CreateOrganizationScopeRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync("organization-scopes", request, WriteOptions, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<OrganizationScope>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for organization-scopes create.");
    }

    public async Task<OrganizationScope> GetAsync(string id, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync($"organization-scopes/{Uri.EscapeDataString(id)}", cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<OrganizationScope>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for organization-scopes get.");
    }

    public async Task<OrganizationScope> UpdateAsync(string id, UpdateOrganizationScopeRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PatchAsJsonAsync($"organization-scopes/{Uri.EscapeDataString(id)}", request, WriteOptions, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<OrganizationScope>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for organization-scopes update.");
    }

    public async Task DeleteAsync(string id, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.DeleteAsync($"organization-scopes/{Uri.EscapeDataString(id)}", cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
    }
}
