using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Logto.ManagementApi.Utils;

namespace Logto.ManagementApi.Organizations;

public sealed class OrganizationRolesClient(HttpClient httpClient) : IOrganizationRolesClient
{
    private static readonly JsonSerializerOptions WriteOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
    };

    public async Task<IReadOnlyList<OrganizationRoleWithScopes>> ListAsync(string? q = null, int? page = null, int? pageSize = null, CancellationToken cancellationToken = default)
    {
        var query = new StringBuilder();
        if (q is not null) query.Append($"q={Uri.EscapeDataString(q)}&");
        if (page is not null) query.Append($"page={page}&");
        if (pageSize is not null) query.Append($"page_size={pageSize}&");
        var url = query.Length > 0 ? $"organization-roles?{query.ToString().TrimEnd('&')}" : "organization-roles";
        var response = await httpClient.GetAsync(url, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<OrganizationRoleWithScopes[]>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for organization-roles list.");
    }

    public async Task<OrganizationRole> CreateAsync(CreateOrganizationRoleRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync("organization-roles", request, WriteOptions, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<OrganizationRole>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for organization-roles create.");
    }

    public async Task<OrganizationRole> GetAsync(string id, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync($"organization-roles/{Uri.EscapeDataString(id)}", cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<OrganizationRole>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for organization-roles get.");
    }

    public async Task<OrganizationRole> UpdateAsync(string id, UpdateOrganizationRoleRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PatchAsJsonAsync($"organization-roles/{Uri.EscapeDataString(id)}", request, WriteOptions, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<OrganizationRole>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for organization-roles update.");
    }

    public async Task DeleteAsync(string id, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.DeleteAsync($"organization-roles/{Uri.EscapeDataString(id)}", cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<OrganizationRoleScope>> ListScopesAsync(string id, int? page = null, int? pageSize = null, CancellationToken cancellationToken = default)
    {
        var url = BuildPagedUrl($"organization-roles/{Uri.EscapeDataString(id)}/scopes", page, pageSize);
        var response = await httpClient.GetAsync(url, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<OrganizationRoleScope[]>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for organization-roles scopes list.");
    }

    public async Task AssignScopesAsync(string id, OrganizationScopeIdsRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync($"organization-roles/{Uri.EscapeDataString(id)}/scopes", request, WriteOptions, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
    }

    public async Task ReplaceScopesAsync(string id, OrganizationScopeIdsRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PutAsJsonAsync($"organization-roles/{Uri.EscapeDataString(id)}/scopes", request, WriteOptions, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
    }

    public async Task RemoveScopeAsync(string id, string organizationScopeId, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.DeleteAsync($"organization-roles/{Uri.EscapeDataString(id)}/scopes/{Uri.EscapeDataString(organizationScopeId)}", cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<OrganizationRoleResourceScope>> ListResourceScopesAsync(string id, int? page = null, int? pageSize = null, CancellationToken cancellationToken = default)
    {
        var url = BuildPagedUrl($"organization-roles/{Uri.EscapeDataString(id)}/resource-scopes", page, pageSize);
        var response = await httpClient.GetAsync(url, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<OrganizationRoleResourceScope[]>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for organization-roles resource-scopes list.");
    }

    public async Task AssignResourceScopesAsync(string id, ResourceScopeIdsRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync($"organization-roles/{Uri.EscapeDataString(id)}/resource-scopes", request, WriteOptions, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
    }

    public async Task ReplaceResourceScopesAsync(string id, ResourceScopeIdsRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PutAsJsonAsync($"organization-roles/{Uri.EscapeDataString(id)}/resource-scopes", request, WriteOptions, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
    }

    public async Task RemoveResourceScopeAsync(string id, string scopeId, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.DeleteAsync($"organization-roles/{Uri.EscapeDataString(id)}/resource-scopes/{Uri.EscapeDataString(scopeId)}", cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
    }

    private static string BuildPagedUrl(string base_, int? page, int? pageSize)
    {
        var query = new StringBuilder();
        if (page is not null) query.Append($"page={page}&");
        if (pageSize is not null) query.Append($"page_size={pageSize}&");
        return query.Length > 0 ? $"{base_}?{query.ToString().TrimEnd('&')}" : base_;
    }
}
