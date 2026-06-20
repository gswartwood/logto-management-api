namespace Logto.ManagementApi.Applications;

public interface IApplicationsClient
{
    Task<IReadOnlyList<Application>> ListAsync(ApplicationsListOptions? options = null, CancellationToken cancellationToken = default);
    Task<Application> CreateAsync(CreateApplicationRequest request, CancellationToken cancellationToken = default);
    Task<Application> GetAsync(string id, CancellationToken cancellationToken = default);
    Task<Application> UpdateAsync(string id, UpdateApplicationRequest request, CancellationToken cancellationToken = default);
    Task DeleteAsync(string id, CancellationToken cancellationToken = default);

    Task<Dictionary<string, object>> UpdateCustomDataAsync(string applicationId, Dictionary<string, object> customData, CancellationToken cancellationToken = default);

    Task<ApplicationAccessControl> GetAccessControlAsync(string applicationId, CancellationToken cancellationToken = default);
    Task<ApplicationAccessControl> UpdateAccessControlAsync(string applicationId, ApplicationAccessControl request, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<ApplicationRole>> ListRolesAsync(string applicationId, int? page = null, int? pageSize = null, CancellationToken cancellationToken = default);
    Task<AssignApplicationRolesResult> AssignRolesAsync(string applicationId, AssignApplicationRolesRequest request, CancellationToken cancellationToken = default);
    Task ReplaceRolesAsync(string applicationId, ReplaceApplicationRolesRequest request, CancellationToken cancellationToken = default);
    Task DeleteRoleAsync(string applicationId, string roleId, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<CustomDomain>> ListProtectedAppCustomDomainsAsync(string id, CancellationToken cancellationToken = default);
    Task AddProtectedAppCustomDomainAsync(string id, AddProtectedAppCustomDomainRequest request, CancellationToken cancellationToken = default);
    Task DeleteProtectedAppCustomDomainAsync(string id, string domain, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<ApplicationOrganization>> ListOrganizationsAsync(string id, int? page = null, int? pageSize = null, CancellationToken cancellationToken = default);

    Task<Application> DeleteLegacySecretAsync(string id, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<ApplicationSecret>> ListSecretsAsync(string id, CancellationToken cancellationToken = default);
    Task<ApplicationSecret> CreateSecretAsync(string id, CreateApplicationSecretRequest request, CancellationToken cancellationToken = default);
    Task DeleteSecretAsync(string id, string name, CancellationToken cancellationToken = default);
    Task<ApplicationSecret> UpdateSecretAsync(string id, string name, UpdateApplicationSecretRequest request, CancellationToken cancellationToken = default);

    Task AssignUserConsentScopesAsync(string applicationId, AssignUserConsentScopesRequest request, CancellationToken cancellationToken = default);
    Task<ApplicationConsentScopes> GetUserConsentScopesAsync(string applicationId, CancellationToken cancellationToken = default);
    Task DeleteUserConsentScopeAsync(string applicationId, UserConsentScopeType scopeType, string scopeId, CancellationToken cancellationToken = default);

    Task<ApplicationSignInExperience> GetSignInExperienceAsync(string applicationId, CancellationToken cancellationToken = default);
    Task<ApplicationSignInExperience> UpdateSignInExperienceAsync(string applicationId, UpdateApplicationSignInExperienceRequest request, CancellationToken cancellationToken = default);

    Task<UserConsentOrganizationsResult> ListUserConsentOrganizationsAsync(string id, string userId, int? page = null, int? pageSize = null, CancellationToken cancellationToken = default);
    Task ReplaceUserConsentOrganizationsAsync(string id, string userId, ReplaceUserConsentOrganizationsRequest request, CancellationToken cancellationToken = default);
    Task AddUserConsentOrganizationsAsync(string id, string userId, AddUserConsentOrganizationsRequest request, CancellationToken cancellationToken = default);
    Task DeleteUserConsentOrganizationAsync(string id, string userId, string organizationId, CancellationToken cancellationToken = default);
}
