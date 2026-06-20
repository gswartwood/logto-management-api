using System.Text.Json.Serialization;

namespace Logto.ManagementApi.Applications;

public record Application
{
    public required string TenantId { get; init; }
    public required string Id { get; init; }
    public required string Name { get; init; }
    public string? Secret { get; init; }
    public string? Description { get; init; }
    public ApplicationType Type { get; init; }
    public OidcClientMetadata? OidcClientMetadata { get; init; }
    public CustomClientMetadata? CustomClientMetadata { get; init; }
    public ProtectedAppMetadata? ProtectedAppMetadata { get; init; }
    public Dictionary<string, object>? CustomData { get; init; }
    public bool IsThirdParty { get; init; }
    public bool AppLevelAccessControlEnabled { get; init; }
    public double CreatedAt { get; init; }
    public bool IsAdmin { get; init; }
}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ApplicationType
{
    Native,
    SPA,
    Traditional,
    MachineToMachine,
    Protected,
    SAML,
}

public record OidcClientMetadata
{
    public List<string>? RedirectUris { get; init; }
    public List<string>? PostLogoutRedirectUris { get; init; }
    public string? BackchannelLogoutUri { get; init; }
    public bool? BackchannelLogoutSessionRequired { get; init; }
    public string? LogoUri { get; init; }
}

public record CustomClientMetadata
{
    public List<string>? CorsAllowedOrigins { get; init; }
    public double? IdTokenTtl { get; init; }
    public double? RefreshTokenTtl { get; init; }
    public double? RefreshTokenTtlInDays { get; init; }
    public string? TenantId { get; init; }
    public bool? AlwaysIssueRefreshToken { get; init; }
    public bool? RotateRefreshToken { get; init; }
    public bool? AllowTokenExchange { get; init; }
    public bool? IsDeviceFlow { get; init; }
    public double? MaxAllowedGrants { get; init; }
}

public record ProtectedAppMetadata
{
    public string? Host { get; init; }
    public string? Origin { get; init; }
    public double? SessionDuration { get; init; }
    public List<PageRule>? PageRules { get; init; }
    public List<string>? AdditionalScopes { get; init; }
    public List<CustomDomain>? CustomDomains { get; init; }
}

public record PageRule
{
    public required string Path { get; init; }
}

public record CustomDomain
{
    public required string Domain { get; init; }
    public CustomDomainStatus Status { get; init; }
    public string? ErrorMessage { get; init; }
    public List<DnsRecord>? DnsRecords { get; init; }
    public CloudflareData? CloudflareData { get; init; }
}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum CustomDomainStatus
{
    PendingVerification,
    PendingSsl,
    Active,
    Error,
}

public record DnsRecord
{
    public required string Name { get; init; }
    public required string Type { get; init; }
    public required string Value { get; init; }
}

public record CloudflareData
{
    public string? Id { get; init; }
    public string? Status { get; init; }
    public CloudflareSsl? Ssl { get; init; }
    public List<string>? VerificationErrors { get; init; }
}

public record CloudflareSsl
{
    public string? Status { get; init; }
    public List<CloudflareValidationError>? ValidationErrors { get; init; }
}

public record CloudflareValidationError
{
    public string? Message { get; init; }
}

public record UpdateApplicationRequest
{
    public string? Name { get; init; }
    public string? Description { get; init; }
    public UpdateOidcClientMetadata? OidcClientMetadata { get; init; }
    public UpdateCustomClientMetadata? CustomClientMetadata { get; init; }
    public Dictionary<string, object>? CustomData { get; init; }
    public bool? AppLevelAccessControlEnabled { get; init; }
    public UpdateProtectedAppMetadata? ProtectedAppMetadata { get; init; }
    public bool? IsAdmin { get; init; }
}

public record UpdateOidcClientMetadata
{
    public required List<string> RedirectUris { get; init; }
    public required List<string> PostLogoutRedirectUris { get; init; }
    public string? BackchannelLogoutUri { get; init; }
    public bool? BackchannelLogoutSessionRequired { get; init; }
    public string? LogoUri { get; init; }
}

public record UpdateCustomClientMetadata
{
    public List<string>? CorsAllowedOrigins { get; init; }
    public double? IdTokenTtl { get; init; }
    public double? RefreshTokenTtl { get; init; }
    public double? RefreshTokenTtlInDays { get; init; }
    public bool? AlwaysIssueRefreshToken { get; init; }
    public bool? RotateRefreshToken { get; init; }
    public bool? AllowTokenExchange { get; init; }
    public bool? IsDeviceFlow { get; init; }
    public double? MaxAllowedGrants { get; init; }
}

public record UpdateProtectedAppMetadata
{
    public string? Origin { get; init; }
    public double? SessionDuration { get; init; }
    public List<PageRule>? PageRules { get; init; }
    public List<string>? AdditionalScopes { get; init; }
}

public record ApplicationsListOptions
{
    public IEnumerable<ApplicationType>? Types { get; init; }
    public string? ExcludeRoleId { get; init; }
    public string? ExcludeOrganizationId { get; init; }
    public bool? IsThirdParty { get; init; }
    public int? Page { get; init; }
    public int? PageSize { get; init; }
    public Dictionary<string, string>? SearchParams { get; init; }
}

public record CreateApplicationRequest(
    string Name,
    ApplicationType Type,
    string? Description = null,
    OidcClientMetadata? OidcClientMetadata = null,
    UpdateCustomClientMetadata? CustomClientMetadata = null,
    Dictionary<string, object>? CustomData = null,
    bool? IsThirdParty = null,
    CreateApplicationProtectedAppMetadata? ProtectedAppMetadata = null);

public record CreateApplicationProtectedAppMetadata(string SubDomain, string Origin);

[JsonConverter(typeof(JsonStringEnumConverter<ApplicationRoleType>))]
public enum ApplicationRoleType { User, MachineToMachine }

public record ApplicationRole(
    string TenantId, string Id, string Name, string Description,
    ApplicationRoleType Type, bool IsDefault);

public record AssignApplicationRolesRequest(IReadOnlyList<string> RoleIds);
public record AssignApplicationRolesResult(IReadOnlyList<string> RoleIds, IReadOnlyList<string> AddedRoleIds);
public record ReplaceApplicationRolesRequest(IReadOnlyList<string> RoleIds);

public record OrganizationRoleRule(string OrganizationId, IReadOnlyList<string> OrganizationRoleIds);

public record ApplicationAccessControl(
    IReadOnlyList<string> UserIds,
    IReadOnlyList<string> UserRoleIds,
    IReadOnlyList<string> OrganizationIds,
    IReadOnlyList<OrganizationRoleRule> OrganizationRoleRules);

public record OrganizationColor(string? PrimaryColor, bool? IsDarkModeEnabled, string? DarkPrimaryColor);
public record OrganizationBranding(string? LogoUrl, string? DarkLogoUrl, string? Favicon, string? DarkFavicon);
public record OrganizationRoleSummary(string Id, string Name);

public record ApplicationOrganization(
    string TenantId, string Id, string Name, string? Description,
    Dictionary<string, object> CustomData, bool IsMfaRequired,
    OrganizationColor? Color, OrganizationBranding? Branding,
    string? CustomCss, double CreatedAt,
    IReadOnlyList<OrganizationRoleSummary> OrganizationRoles);

public record ApplicationSecret(
    string TenantId, string ApplicationId, string Name, string Value,
    double CreatedAt, double? ExpiresAt);

public record CreateApplicationSecretRequest(string Name, double? ExpiresAt = null);
public record UpdateApplicationSecretRequest(string Name);

public record AddProtectedAppCustomDomainRequest(string Domain);

public record AssignUserConsentScopesRequest(
    IReadOnlyList<string>? OrganizationScopes = null,
    IReadOnlyList<string>? ResourceScopes = null,
    IReadOnlyList<string>? OrganizationResourceScopes = null,
    IReadOnlyList<string>? UserScopes = null);

public record ConsentOrganizationScope(string Id, string Name, string? Description);
public record ConsentResource(string Id, string Name, string Indicator);
public record ConsentScope(string Id, string Name, string? Description);
public record ConsentResourceScopeGroup(ConsentResource Resource, IReadOnlyList<ConsentScope> Scopes);

public record ApplicationConsentScopes(
    IReadOnlyList<ConsentOrganizationScope> OrganizationScopes,
    IReadOnlyList<ConsentResourceScopeGroup> ResourceScopes,
    IReadOnlyList<ConsentResourceScopeGroup> OrganizationResourceScopes,
    IReadOnlyList<string> UserScopes);

public enum UserConsentScopeType { OrganizationScopes, ResourceScopes, OrganizationResourceScopes, UserScopes }

public record ApplicationSieColor(string? PrimaryColor, bool? IsDarkModeEnabled, string? DarkPrimaryColor);
public record ApplicationSieBranding(string? LogoUrl, string? DarkLogoUrl, string? Favicon, string? DarkFavicon);

public record ApplicationSignInExperience(
    string TenantId, string ApplicationId,
    ApplicationSieColor? Color, ApplicationSieBranding? Branding,
    string? CustomCss, string? TermsOfUseUrl, string? PrivacyPolicyUrl, string? DisplayName);

public record UpdateApplicationSignInExperienceRequest(
    string TermsOfUseUrl,
    string PrivacyPolicyUrl,
    ApplicationSieColor? Color = null,
    ApplicationSieBranding? Branding = null,
    string? CustomCss = null,
    string? DisplayName = null);

public record UserConsentOrganization(
    string TenantId, string Id, string Name, string? Description,
    Dictionary<string, object> CustomData, bool IsMfaRequired,
    OrganizationColor? Color, OrganizationBranding? Branding,
    string? CustomCss, double CreatedAt);

public record UserConsentOrganizationsResult(IReadOnlyList<UserConsentOrganization> Organizations);

public record ReplaceUserConsentOrganizationsRequest(IReadOnlyList<string> OrganizationIds);
public record AddUserConsentOrganizationsRequest(IReadOnlyList<string> OrganizationIds);
