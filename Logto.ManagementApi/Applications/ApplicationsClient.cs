using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Logto.ManagementApi.Utils;

namespace Logto.ManagementApi.Applications;

public sealed class ApplicationsClient(HttpClient httpClient) : IApplicationsClient
{
    private static readonly JsonSerializerOptions WriteOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
    };

    public async Task<IReadOnlyList<Application>> ListAsync(ApplicationsListOptions? options = null, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync(BuildListUrl(options), cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<IReadOnlyList<Application>>(cancellationToken: cancellationToken) ?? [];
    }

    public async Task<Application> CreateAsync(CreateApplicationRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync("applications", request, WriteOptions, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<Application>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for application create.");
    }

    public async Task<Application> GetAsync(string id, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync($"applications/{Uri.EscapeDataString(id)}", cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<Application>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for application get.");
    }

    public async Task<Application> UpdateAsync(string id, UpdateApplicationRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PatchAsJsonAsync($"applications/{Uri.EscapeDataString(id)}", request, WriteOptions, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<Application>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for application update.");
    }

    public async Task DeleteAsync(string id, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.DeleteAsync($"applications/{Uri.EscapeDataString(id)}", cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
    }

    public async Task<Dictionary<string, object>> UpdateCustomDataAsync(string applicationId, Dictionary<string, object> customData, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PatchAsJsonAsync($"applications/{Uri.EscapeDataString(applicationId)}/custom-data", customData, WriteOptions, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<Dictionary<string, object>>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for application custom-data update.");
    }

    public async Task<ApplicationAccessControl> GetAccessControlAsync(string applicationId, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync($"applications/{Uri.EscapeDataString(applicationId)}/access-control", cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<ApplicationAccessControl>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for application access-control get.");
    }

    public async Task<ApplicationAccessControl> UpdateAccessControlAsync(string applicationId, ApplicationAccessControl request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PutAsJsonAsync($"applications/{Uri.EscapeDataString(applicationId)}/access-control", request, WriteOptions, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<ApplicationAccessControl>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for application access-control update.");
    }

    public async Task<IReadOnlyList<ApplicationRole>> ListRolesAsync(string applicationId, int? page = null, int? pageSize = null, CancellationToken cancellationToken = default)
    {
        var url = BuildPagedUrl($"applications/{Uri.EscapeDataString(applicationId)}/roles", page, pageSize);
        var response = await httpClient.GetAsync(url, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<IReadOnlyList<ApplicationRole>>(cancellationToken: cancellationToken) ?? [];
    }

    public async Task<AssignApplicationRolesResult> AssignRolesAsync(string applicationId, AssignApplicationRolesRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync($"applications/{Uri.EscapeDataString(applicationId)}/roles", request, WriteOptions, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<AssignApplicationRolesResult>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for application roles assign.");
    }

    public async Task ReplaceRolesAsync(string applicationId, ReplaceApplicationRolesRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PutAsJsonAsync($"applications/{Uri.EscapeDataString(applicationId)}/roles", request, WriteOptions, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
    }

    public async Task DeleteRoleAsync(string applicationId, string roleId, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.DeleteAsync($"applications/{Uri.EscapeDataString(applicationId)}/roles/{Uri.EscapeDataString(roleId)}", cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<CustomDomain>> ListProtectedAppCustomDomainsAsync(string id, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync($"applications/{Uri.EscapeDataString(id)}/protected-app-metadata/custom-domains", cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<IReadOnlyList<CustomDomain>>(cancellationToken: cancellationToken) ?? [];
    }

    public async Task AddProtectedAppCustomDomainAsync(string id, AddProtectedAppCustomDomainRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync($"applications/{Uri.EscapeDataString(id)}/protected-app-metadata/custom-domains", request, WriteOptions, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
    }

    public async Task DeleteProtectedAppCustomDomainAsync(string id, string domain, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.DeleteAsync($"applications/{Uri.EscapeDataString(id)}/protected-app-metadata/custom-domains/{Uri.EscapeDataString(domain)}", cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<ApplicationOrganization>> ListOrganizationsAsync(string id, int? page = null, int? pageSize = null, CancellationToken cancellationToken = default)
    {
        var url = BuildPagedUrl($"applications/{Uri.EscapeDataString(id)}/organizations", page, pageSize);
        var response = await httpClient.GetAsync(url, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<IReadOnlyList<ApplicationOrganization>>(cancellationToken: cancellationToken) ?? [];
    }

    public async Task<Application> DeleteLegacySecretAsync(string id, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.DeleteAsync($"applications/{Uri.EscapeDataString(id)}/legacy-secret", cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<Application>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for application legacy-secret delete.");
    }

    public async Task<IReadOnlyList<ApplicationSecret>> ListSecretsAsync(string id, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync($"applications/{Uri.EscapeDataString(id)}/secrets", cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<IReadOnlyList<ApplicationSecret>>(cancellationToken: cancellationToken) ?? [];
    }

    public async Task<ApplicationSecret> CreateSecretAsync(string id, CreateApplicationSecretRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync($"applications/{Uri.EscapeDataString(id)}/secrets", request, WriteOptions, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<ApplicationSecret>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for application secret create.");
    }

    public async Task DeleteSecretAsync(string id, string name, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.DeleteAsync($"applications/{Uri.EscapeDataString(id)}/secrets/{Uri.EscapeDataString(name)}", cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
    }

    public async Task<ApplicationSecret> UpdateSecretAsync(string id, string name, UpdateApplicationSecretRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PatchAsJsonAsync($"applications/{Uri.EscapeDataString(id)}/secrets/{Uri.EscapeDataString(name)}", request, WriteOptions, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<ApplicationSecret>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for application secret update.");
    }

    public async Task AssignUserConsentScopesAsync(string applicationId, AssignUserConsentScopesRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync($"applications/{Uri.EscapeDataString(applicationId)}/user-consent-scopes", request, WriteOptions, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
    }

    public async Task<ApplicationConsentScopes> GetUserConsentScopesAsync(string applicationId, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync($"applications/{Uri.EscapeDataString(applicationId)}/user-consent-scopes", cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<ApplicationConsentScopes>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for application user-consent-scopes get.");
    }

    public async Task DeleteUserConsentScopeAsync(string applicationId, UserConsentScopeType scopeType, string scopeId, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.DeleteAsync(
            $"applications/{Uri.EscapeDataString(applicationId)}/user-consent-scopes/{ToPathString(scopeType)}/{Uri.EscapeDataString(scopeId)}",
            cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
    }

    public async Task<ApplicationSignInExperience> GetSignInExperienceAsync(string applicationId, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync($"applications/{Uri.EscapeDataString(applicationId)}/sign-in-experience", cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<ApplicationSignInExperience>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for application sign-in-experience get.");
    }

    public async Task<ApplicationSignInExperience> UpdateSignInExperienceAsync(string applicationId, UpdateApplicationSignInExperienceRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PutAsJsonAsync($"applications/{Uri.EscapeDataString(applicationId)}/sign-in-experience", request, WriteOptions, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<ApplicationSignInExperience>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for application sign-in-experience update.");
    }

    public async Task<UserConsentOrganizationsResult> ListUserConsentOrganizationsAsync(string id, string userId, int? page = null, int? pageSize = null, CancellationToken cancellationToken = default)
    {
        var url = BuildPagedUrl($"applications/{Uri.EscapeDataString(id)}/users/{Uri.EscapeDataString(userId)}/consent-organizations", page, pageSize);
        var response = await httpClient.GetAsync(url, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<UserConsentOrganizationsResult>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for application user consent-organizations list.");
    }

    public async Task ReplaceUserConsentOrganizationsAsync(string id, string userId, ReplaceUserConsentOrganizationsRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PutAsJsonAsync($"applications/{Uri.EscapeDataString(id)}/users/{Uri.EscapeDataString(userId)}/consent-organizations", request, WriteOptions, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
    }

    public async Task AddUserConsentOrganizationsAsync(string id, string userId, AddUserConsentOrganizationsRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync($"applications/{Uri.EscapeDataString(id)}/users/{Uri.EscapeDataString(userId)}/consent-organizations", request, WriteOptions, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
    }

    public async Task DeleteUserConsentOrganizationAsync(string id, string userId, string organizationId, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.DeleteAsync(
            $"applications/{Uri.EscapeDataString(id)}/users/{Uri.EscapeDataString(userId)}/consent-organizations/{Uri.EscapeDataString(organizationId)}",
            cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
    }

    private static string ToPathString(UserConsentScopeType scopeType) => scopeType switch
    {
        UserConsentScopeType.OrganizationScopes => "organization-scopes",
        UserConsentScopeType.ResourceScopes => "resource-scopes",
        UserConsentScopeType.OrganizationResourceScopes => "organization-resource-scopes",
        UserConsentScopeType.UserScopes => "user-scopes",
        _ => throw new ArgumentOutOfRangeException(nameof(scopeType)),
    };

    private static string BuildPagedUrl(string baseUrl, int? page, int? pageSize)
    {
        if (page is null && pageSize is null)
            return baseUrl;

        var query = new StringBuilder('?');
        if (page is not null)
            query.Append($"page={page}");
        if (pageSize is not null)
        {
            if (query.Length > 1) query.Append('&');
            query.Append($"page_size={pageSize}");
        }
        return $"{baseUrl}{query}";
    }

    private static string BuildListUrl(ApplicationsListOptions? options)
    {
        if (options is null)
            return "applications";

        var query = new StringBuilder();

        void Append(string key, string value)
        {
            query.Append(query.Length == 0 ? '?' : '&');
            query.Append(Uri.EscapeDataString(key));
            query.Append('=');
            query.Append(Uri.EscapeDataString(value));
        }

        if (options.Types is not null)
            foreach (var type in options.Types)
                Append("types", type.ToString());

        if (options.ExcludeRoleId is not null)
            Append("excludeRoleId", options.ExcludeRoleId);

        if (options.ExcludeOrganizationId is not null)
            Append("excludeOrganizationId", options.ExcludeOrganizationId);

        if (options.IsThirdParty is not null)
            Append("isThirdParty", options.IsThirdParty.Value ? "true" : "false");

        if (options.Page is not null)
            Append("page", options.Page.Value.ToString());

        if (options.PageSize is not null)
            Append("page_size", options.PageSize.Value.ToString());

        if (options.SearchParams is not null)
            foreach (var (key, value) in options.SearchParams)
                Append($"search_params[{key}]", value);

        return $"applications{query}";
    }
}
