namespace Logto.ManagementApi.Organizations;

public interface IOrganizationsClient
{
    // CRUD
    Task<IReadOnlyList<Organization>> ListAsync(string? q = null, bool? showFeatured = null, int? page = null, int? pageSize = null, CancellationToken cancellationToken = default);
    Task<Organization> CreateAsync(CreateOrganizationRequest request, CancellationToken cancellationToken = default);
    Task<Organization> GetAsync(string id, CancellationToken cancellationToken = default);
    Task<Organization> UpdateAsync(string id, UpdateOrganizationRequest request, CancellationToken cancellationToken = default);
    Task DeleteAsync(string id, CancellationToken cancellationToken = default);

    // Users
    Task<IReadOnlyList<OrganizationUser>> ListUsersAsync(string id, string? q = null, string? organizationRoleId = null, int? page = null, int? pageSize = null, CancellationToken cancellationToken = default);
    Task<AddOrganizationUsersResult> AddUsersAsync(string id, AddOrganizationUsersRequest request, CancellationToken cancellationToken = default);
    Task ReplaceUsersAsync(string id, ReplaceOrganizationUsersRequest request, CancellationToken cancellationToken = default);
    Task DeleteUserAsync(string id, string userId, CancellationToken cancellationToken = default);

    // User roles (bulk)
    Task BulkAssignUserRolesAsync(string id, BulkAssignOrganizationUserRolesRequest request, CancellationToken cancellationToken = default);

    // User roles (per user)
    Task<IReadOnlyList<OrganizationRole>> ListUserRolesAsync(string id, string userId, int? page = null, int? pageSize = null, CancellationToken cancellationToken = default);
    Task<AssignOrganizationUserRolesResult> AssignUserRolesAsync(string id, string userId, AssignOrganizationUserRolesRequest request, CancellationToken cancellationToken = default);
    Task ReplaceUserRolesAsync(string id, string userId, ReplaceOrganizationUserRolesRequest request, CancellationToken cancellationToken = default);
    Task DeleteUserRoleAsync(string id, string userId, string organizationRoleId, CancellationToken cancellationToken = default);

    // User scopes
    Task<IReadOnlyList<OrganizationUserScope>> ListUserScopesAsync(string id, string userId, CancellationToken cancellationToken = default);

    // Applications
    Task<IReadOnlyList<OrganizationApplication>> ListApplicationsAsync(string id, string? q = null, int? page = null, int? pageSize = null, CancellationToken cancellationToken = default);
    Task AddApplicationsAsync(string id, AddOrganizationApplicationsRequest request, CancellationToken cancellationToken = default);
    Task ReplaceApplicationsAsync(string id, ReplaceOrganizationApplicationsRequest request, CancellationToken cancellationToken = default);
    Task DeleteApplicationAsync(string id, string applicationId, CancellationToken cancellationToken = default);

    // Application roles (bulk)
    Task BulkAssignApplicationRolesAsync(string id, BulkAssignOrganizationApplicationRolesRequest request, CancellationToken cancellationToken = default);

    // Application roles (per application)
    Task<IReadOnlyList<OrganizationRole>> ListApplicationRolesAsync(string id, string applicationId, int? page = null, int? pageSize = null, CancellationToken cancellationToken = default);
    Task AssignApplicationRolesAsync(string id, string applicationId, AssignOrganizationApplicationRolesRequest request, CancellationToken cancellationToken = default);
    Task ReplaceApplicationRolesAsync(string id, string applicationId, ReplaceOrganizationApplicationRolesRequest request, CancellationToken cancellationToken = default);
    Task DeleteApplicationRoleAsync(string id, string applicationId, string organizationRoleId, CancellationToken cancellationToken = default);

    // JIT email domains
    Task<IReadOnlyList<OrganizationJitEmailDomain>> ListJitEmailDomainsAsync(string id, int? page = null, int? pageSize = null, CancellationToken cancellationToken = default);
    Task<OrganizationJitEmailDomain> AddJitEmailDomainAsync(string id, AddJitEmailDomainRequest request, CancellationToken cancellationToken = default);
    Task ReplaceJitEmailDomainsAsync(string id, ReplaceJitEmailDomainsRequest request, CancellationToken cancellationToken = default);
    Task DeleteJitEmailDomainAsync(string id, string emailDomain, CancellationToken cancellationToken = default);

    // JIT roles
    Task<IReadOnlyList<OrganizationRole>> ListJitRolesAsync(string id, int? page = null, int? pageSize = null, CancellationToken cancellationToken = default);
    Task AddJitRolesAsync(string id, AddJitRolesRequest request, CancellationToken cancellationToken = default);
    Task ReplaceJitRolesAsync(string id, ReplaceJitRolesRequest request, CancellationToken cancellationToken = default);
    Task DeleteJitRoleAsync(string id, string organizationRoleId, CancellationToken cancellationToken = default);

    // JIT SSO connectors
    Task<IReadOnlyList<OrganizationJitSsoConnector>> ListJitSsoConnectorsAsync(string id, int? page = null, int? pageSize = null, CancellationToken cancellationToken = default);
    Task AddJitSsoConnectorsAsync(string id, AddJitSsoConnectorsRequest request, CancellationToken cancellationToken = default);
    Task ReplaceJitSsoConnectorsAsync(string id, ReplaceJitSsoConnectorsRequest request, CancellationToken cancellationToken = default);
    Task DeleteJitSsoConnectorAsync(string id, string ssoConnectorId, CancellationToken cancellationToken = default);
}
