using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Logto.ManagementApi.Utils;

namespace Logto.ManagementApi.Organizations;

public sealed class OrganizationsClient(HttpClient httpClient) : IOrganizationsClient
{
    private static readonly JsonSerializerOptions WriteOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
    };

    // CRUD

    public async Task<IReadOnlyList<Organization>> ListAsync(string? q = null, bool? showFeatured = null, int? page = null, int? pageSize = null, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync(BuildOrganizationsListUrl(q, showFeatured, page, pageSize), cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<IReadOnlyList<Organization>>(cancellationToken: cancellationToken) ?? [];
    }

    public async Task<Organization> CreateAsync(CreateOrganizationRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync("organizations", request, WriteOptions, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<Organization>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for organization create.");
    }

    public async Task<Organization> GetAsync(string id, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync($"organizations/{Uri.EscapeDataString(id)}", cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<Organization>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for organization get.");
    }

    public async Task<Organization> UpdateAsync(string id, UpdateOrganizationRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PatchAsJsonAsync($"organizations/{Uri.EscapeDataString(id)}", request, WriteOptions, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<Organization>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for organization update.");
    }

    public async Task DeleteAsync(string id, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.DeleteAsync($"organizations/{Uri.EscapeDataString(id)}", cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
    }

    // Users

    public async Task<IReadOnlyList<OrganizationUser>> ListUsersAsync(string id, string? q = null, string? organizationRoleId = null, int? page = null, int? pageSize = null, CancellationToken cancellationToken = default)
    {
        var url = BuildUrl($"organizations/{Uri.EscapeDataString(id)}/users", q: q, organizationRoleId: organizationRoleId, page: page, pageSize: pageSize);
        var response = await httpClient.GetAsync(url, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<IReadOnlyList<OrganizationUser>>(cancellationToken: cancellationToken) ?? [];
    }

    public async Task<AddOrganizationUsersResult> AddUsersAsync(string id, AddOrganizationUsersRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync($"organizations/{Uri.EscapeDataString(id)}/users", request, WriteOptions, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<AddOrganizationUsersResult>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for organization users add.");
    }

    public async Task ReplaceUsersAsync(string id, ReplaceOrganizationUsersRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PutAsJsonAsync($"organizations/{Uri.EscapeDataString(id)}/users", request, WriteOptions, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
    }

    public async Task DeleteUserAsync(string id, string userId, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.DeleteAsync($"organizations/{Uri.EscapeDataString(id)}/users/{Uri.EscapeDataString(userId)}", cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
    }

    // User roles (bulk)

    public async Task BulkAssignUserRolesAsync(string id, BulkAssignOrganizationUserRolesRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync($"organizations/{Uri.EscapeDataString(id)}/users/roles", request, WriteOptions, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
    }

    // User roles (per user)

    public async Task<IReadOnlyList<OrganizationRole>> ListUserRolesAsync(string id, string userId, int? page = null, int? pageSize = null, CancellationToken cancellationToken = default)
    {
        var url = BuildPagedUrl($"organizations/{Uri.EscapeDataString(id)}/users/{Uri.EscapeDataString(userId)}/roles", page, pageSize);
        var response = await httpClient.GetAsync(url, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<IReadOnlyList<OrganizationRole>>(cancellationToken: cancellationToken) ?? [];
    }

    public async Task<AssignOrganizationUserRolesResult> AssignUserRolesAsync(string id, string userId, AssignOrganizationUserRolesRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync($"organizations/{Uri.EscapeDataString(id)}/users/{Uri.EscapeDataString(userId)}/roles", request, WriteOptions, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<AssignOrganizationUserRolesResult>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for organization user roles assign.");
    }

    public async Task ReplaceUserRolesAsync(string id, string userId, ReplaceOrganizationUserRolesRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PutAsJsonAsync($"organizations/{Uri.EscapeDataString(id)}/users/{Uri.EscapeDataString(userId)}/roles", request, WriteOptions, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
    }

    public async Task DeleteUserRoleAsync(string id, string userId, string organizationRoleId, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.DeleteAsync(
            $"organizations/{Uri.EscapeDataString(id)}/users/{Uri.EscapeDataString(userId)}/roles/{Uri.EscapeDataString(organizationRoleId)}",
            cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
    }

    // User scopes

    public async Task<IReadOnlyList<OrganizationUserScope>> ListUserScopesAsync(string id, string userId, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync($"organizations/{Uri.EscapeDataString(id)}/users/{Uri.EscapeDataString(userId)}/scopes", cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<IReadOnlyList<OrganizationUserScope>>(cancellationToken: cancellationToken) ?? [];
    }

    // Applications

    public async Task<IReadOnlyList<OrganizationApplication>> ListApplicationsAsync(string id, string? q = null, int? page = null, int? pageSize = null, CancellationToken cancellationToken = default)
    {
        var url = BuildUrl($"organizations/{Uri.EscapeDataString(id)}/applications", q: q, page: page, pageSize: pageSize);
        var response = await httpClient.GetAsync(url, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<IReadOnlyList<OrganizationApplication>>(cancellationToken: cancellationToken) ?? [];
    }

    public async Task AddApplicationsAsync(string id, AddOrganizationApplicationsRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync($"organizations/{Uri.EscapeDataString(id)}/applications", request, WriteOptions, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
    }

    public async Task ReplaceApplicationsAsync(string id, ReplaceOrganizationApplicationsRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PutAsJsonAsync($"organizations/{Uri.EscapeDataString(id)}/applications", request, WriteOptions, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
    }

    public async Task DeleteApplicationAsync(string id, string applicationId, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.DeleteAsync($"organizations/{Uri.EscapeDataString(id)}/applications/{Uri.EscapeDataString(applicationId)}", cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
    }

    // Application roles (bulk)

    public async Task BulkAssignApplicationRolesAsync(string id, BulkAssignOrganizationApplicationRolesRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync($"organizations/{Uri.EscapeDataString(id)}/applications/roles", request, WriteOptions, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
    }

    // Application roles (per application)

    public async Task<IReadOnlyList<OrganizationRole>> ListApplicationRolesAsync(string id, string applicationId, int? page = null, int? pageSize = null, CancellationToken cancellationToken = default)
    {
        var url = BuildPagedUrl($"organizations/{Uri.EscapeDataString(id)}/applications/{Uri.EscapeDataString(applicationId)}/roles", page, pageSize);
        var response = await httpClient.GetAsync(url, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<IReadOnlyList<OrganizationRole>>(cancellationToken: cancellationToken) ?? [];
    }

    public async Task AssignApplicationRolesAsync(string id, string applicationId, AssignOrganizationApplicationRolesRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync($"organizations/{Uri.EscapeDataString(id)}/applications/{Uri.EscapeDataString(applicationId)}/roles", request, WriteOptions, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
    }

    public async Task ReplaceApplicationRolesAsync(string id, string applicationId, ReplaceOrganizationApplicationRolesRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PutAsJsonAsync($"organizations/{Uri.EscapeDataString(id)}/applications/{Uri.EscapeDataString(applicationId)}/roles", request, WriteOptions, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
    }

    public async Task DeleteApplicationRoleAsync(string id, string applicationId, string organizationRoleId, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.DeleteAsync(
            $"organizations/{Uri.EscapeDataString(id)}/applications/{Uri.EscapeDataString(applicationId)}/roles/{Uri.EscapeDataString(organizationRoleId)}",
            cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
    }

    // JIT email domains

    public async Task<IReadOnlyList<OrganizationJitEmailDomain>> ListJitEmailDomainsAsync(string id, int? page = null, int? pageSize = null, CancellationToken cancellationToken = default)
    {
        var url = BuildPagedUrl($"organizations/{Uri.EscapeDataString(id)}/jit/email-domains", page, pageSize);
        var response = await httpClient.GetAsync(url, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<IReadOnlyList<OrganizationJitEmailDomain>>(cancellationToken: cancellationToken) ?? [];
    }

    public async Task<OrganizationJitEmailDomain> AddJitEmailDomainAsync(string id, AddJitEmailDomainRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync($"organizations/{Uri.EscapeDataString(id)}/jit/email-domains", request, WriteOptions, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<OrganizationJitEmailDomain>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for JIT email domain add.");
    }

    public async Task ReplaceJitEmailDomainsAsync(string id, ReplaceJitEmailDomainsRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PutAsJsonAsync($"organizations/{Uri.EscapeDataString(id)}/jit/email-domains", request, WriteOptions, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
    }

    public async Task DeleteJitEmailDomainAsync(string id, string emailDomain, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.DeleteAsync($"organizations/{Uri.EscapeDataString(id)}/jit/email-domains/{Uri.EscapeDataString(emailDomain)}", cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
    }

    // JIT roles

    public async Task<IReadOnlyList<OrganizationRole>> ListJitRolesAsync(string id, int? page = null, int? pageSize = null, CancellationToken cancellationToken = default)
    {
        var url = BuildPagedUrl($"organizations/{Uri.EscapeDataString(id)}/jit/roles", page, pageSize);
        var response = await httpClient.GetAsync(url, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<IReadOnlyList<OrganizationRole>>(cancellationToken: cancellationToken) ?? [];
    }

    public async Task AddJitRolesAsync(string id, AddJitRolesRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync($"organizations/{Uri.EscapeDataString(id)}/jit/roles", request, WriteOptions, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
    }

    public async Task ReplaceJitRolesAsync(string id, ReplaceJitRolesRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PutAsJsonAsync($"organizations/{Uri.EscapeDataString(id)}/jit/roles", request, WriteOptions, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
    }

    public async Task DeleteJitRoleAsync(string id, string organizationRoleId, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.DeleteAsync($"organizations/{Uri.EscapeDataString(id)}/jit/roles/{Uri.EscapeDataString(organizationRoleId)}", cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
    }

    // JIT SSO connectors

    public async Task<IReadOnlyList<OrganizationJitSsoConnector>> ListJitSsoConnectorsAsync(string id, int? page = null, int? pageSize = null, CancellationToken cancellationToken = default)
    {
        var url = BuildPagedUrl($"organizations/{Uri.EscapeDataString(id)}/jit/sso-connectors", page, pageSize);
        var response = await httpClient.GetAsync(url, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<IReadOnlyList<OrganizationJitSsoConnector>>(cancellationToken: cancellationToken) ?? [];
    }

    public async Task AddJitSsoConnectorsAsync(string id, AddJitSsoConnectorsRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync($"organizations/{Uri.EscapeDataString(id)}/jit/sso-connectors", request, WriteOptions, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
    }

    public async Task ReplaceJitSsoConnectorsAsync(string id, ReplaceJitSsoConnectorsRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PutAsJsonAsync($"organizations/{Uri.EscapeDataString(id)}/jit/sso-connectors", request, WriteOptions, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
    }

    public async Task DeleteJitSsoConnectorAsync(string id, string ssoConnectorId, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.DeleteAsync($"organizations/{Uri.EscapeDataString(id)}/jit/sso-connectors/{Uri.EscapeDataString(ssoConnectorId)}", cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
    }

    private static string BuildOrganizationsListUrl(string? q, bool? showFeatured, int? page, int? pageSize)
    {
        var query = new StringBuilder();

        void Append(string key, string value)
        {
            query.Append(query.Length == 0 ? '?' : '&');
            query.Append(Uri.EscapeDataString(key));
            query.Append('=');
            query.Append(Uri.EscapeDataString(value));
        }

        if (q is not null) Append("q", q);
        if (showFeatured is not null) Append("showFeatured", showFeatured.Value ? "true" : "false");
        if (page is not null) Append("page", page.Value.ToString());
        if (pageSize is not null) Append("page_size", pageSize.Value.ToString());

        return $"organizations{query}";
    }

    private static string BuildUrl(string baseUrl, string? q = null, string? organizationRoleId = null, int? page = null, int? pageSize = null)
    {
        var query = new StringBuilder();

        void Append(string key, string value)
        {
            query.Append(query.Length == 0 ? '?' : '&');
            query.Append(Uri.EscapeDataString(key));
            query.Append('=');
            query.Append(Uri.EscapeDataString(value));
        }

        if (q is not null) Append("q", q);
        if (organizationRoleId is not null) Append("organizationRoleId", organizationRoleId);
        if (page is not null) Append("page", page.Value.ToString());
        if (pageSize is not null) Append("page_size", pageSize.Value.ToString());

        return query.Length == 0 ? baseUrl : $"{baseUrl}{query}";
    }

    private static string BuildPagedUrl(string baseUrl, int? page, int? pageSize)
    {
        if (page is null && pageSize is null) return baseUrl;
        var query = new StringBuilder('?');
        if (page is not null) query.Append($"page={page}");
        if (pageSize is not null)
        {
            if (query.Length > 1) query.Append('&');
            query.Append($"page_size={pageSize}");
        }
        return $"{baseUrl}{query}";
    }
}
