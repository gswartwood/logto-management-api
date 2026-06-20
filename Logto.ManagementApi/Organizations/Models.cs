using System.Text.Json;
using System.Text.Json.Serialization;

namespace Logto.ManagementApi.Organizations;

public record OrganizationColor(string? PrimaryColor, bool? IsDarkModeEnabled, string? DarkPrimaryColor);
public record OrganizationBranding(string? LogoUrl, string? DarkLogoUrl, string? Favicon, string? DarkFavicon);
public record OrganizationFeaturedUser(string Id, string? Avatar, string? Name);

public record Organization(
    string TenantId, string Id, string Name, string? Description,
    Dictionary<string, object> CustomData, bool IsMfaRequired,
    OrganizationColor? Color, OrganizationBranding? Branding,
    string? CustomCss, double CreatedAt,
    double? UsersCount = null,
    IReadOnlyList<OrganizationFeaturedUser>? FeaturedUsers = null);

public record CreateOrganizationRequest(
    string Name,
    string? Description = null,
    Dictionary<string, object>? CustomData = null,
    bool? IsMfaRequired = null,
    OrganizationColor? Color = null,
    OrganizationBranding? Branding = null,
    string? CustomCss = null);

public record UpdateOrganizationRequest(
    string? Name = null,
    string? Description = null,
    Dictionary<string, object>? CustomData = null,
    bool? IsMfaRequired = null,
    OrganizationColor? Color = null,
    OrganizationBranding? Branding = null,
    string? CustomCss = null);

[JsonConverter(typeof(JsonStringEnumConverter<OrganizationRoleType>))]
public enum OrganizationRoleType { User, MachineToMachine }

public record OrganizationRoleSummary(string Id, string Name);

public record OrganizationRole(
    string TenantId, string Id, string Name, string? Description, OrganizationRoleType Type);

public record OrganizationUserAddress(
    string? Formatted, string? StreetAddress, string? Locality,
    string? Region, string? PostalCode, string? Country);

public record OrganizationUserProfile(
    string? FamilyName, string? GivenName, string? MiddleName, string? Nickname,
    string? PreferredUsername, string? Profile, string? Website, string? Gender,
    string? Birthdate, string? Zoneinfo, string? Locale, OrganizationUserAddress? Address);

public record OrganizationUserIdentityEntry(string UserId, Dictionary<string, object>? Details);

public record OrganizationUser(
    string Id, string? Username, string? PrimaryEmail, string? PrimaryPhone,
    string? Name, string? Avatar, Dictionary<string, object> CustomData,
    Dictionary<string, OrganizationUserIdentityEntry>? Identities,
    double? LastSignInAt, double CreatedAt, double UpdatedAt,
    OrganizationUserProfile? Profile, string? ApplicationId,
    bool IsSuspended, IReadOnlyList<OrganizationRoleSummary> OrganizationRoles);

public record AddOrganizationUsersRequest(IReadOnlyList<string> UserIds);
public record AddOrganizationUsersResult(IReadOnlyList<string> UserIds);
public record ReplaceOrganizationUsersRequest(IReadOnlyList<string> UserIds);

public record BulkAssignOrganizationUserRolesRequest(
    IReadOnlyList<string> UserIds, IReadOnlyList<string> OrganizationRoleIds);

public record AssignOrganizationUserRolesRequest(
    IReadOnlyList<string>? OrganizationRoleIds = null,
    IReadOnlyList<string>? OrganizationRoleNames = null);

public record AssignOrganizationUserRolesResult(IReadOnlyList<string> OrganizationRoleIds);

public record ReplaceOrganizationUserRolesRequest(
    IReadOnlyList<string>? OrganizationRoleIds = null,
    IReadOnlyList<string>? OrganizationRoleNames = null);

public record OrganizationUserScope(string TenantId, string Id, string Name, string? Description);

public record OrganizationApplication(
    string TenantId, string Id, string Name, string? Secret, string? Description,
    string? Type, JsonElement? OidcClientMetadata, JsonElement? CustomClientMetadata,
    JsonElement? ProtectedAppMetadata, Dictionary<string, object>? CustomData,
    bool IsThirdParty, bool AppLevelAccessControlEnabled, double CreatedAt,
    IReadOnlyList<OrganizationRoleSummary> OrganizationRoles);

public record AddOrganizationApplicationsRequest(IReadOnlyList<string> ApplicationIds);
public record ReplaceOrganizationApplicationsRequest(IReadOnlyList<string> ApplicationIds);

public record BulkAssignOrganizationApplicationRolesRequest(
    IReadOnlyList<string> ApplicationIds, IReadOnlyList<string> OrganizationRoleIds);

public record AssignOrganizationApplicationRolesRequest(IReadOnlyList<string> OrganizationRoleIds);
public record ReplaceOrganizationApplicationRolesRequest(IReadOnlyList<string> OrganizationRoleIds);

public record OrganizationJitEmailDomain(string TenantId, string OrganizationId, string EmailDomain);
public record AddJitEmailDomainRequest(string EmailDomain);
public record ReplaceJitEmailDomainsRequest(IReadOnlyList<string> EmailDomains);

public record AddJitRolesRequest(IReadOnlyList<string> OrganizationRoleIds);
public record ReplaceJitRolesRequest(IReadOnlyList<string> OrganizationRoleIds);

public record AddJitSsoConnectorsRequest(IReadOnlyList<string> SsoConnectorIds);
public record ReplaceJitSsoConnectorsRequest(IReadOnlyList<string> SsoConnectorIds);

public record JitSsoConnectorBranding(string? DisplayName, string? Logo, string? DarkLogo);

public record OrganizationJitSsoConnector(
    string TenantId, string Id, string ProviderName, string ConnectorName,
    Dictionary<string, object> Config, IReadOnlyList<string> Domains,
    JitSsoConnectorBranding? Branding, bool SyncProfile, bool EnableTokenStorage, double CreatedAt);

// Organization Invitations

[JsonConverter(typeof(JsonStringEnumConverter<OrganizationInvitationStatus>))]
public enum OrganizationInvitationStatus { Pending, Accepted, Expired, Revoked }

public record OrganizationInvitationRole(string Id, string Name);

public record OrganizationInvitation(
    string TenantId,
    string Id,
    string? InviterId,
    string Invitee,
    string? AcceptedUserId,
    string OrganizationId,
    OrganizationInvitationStatus Status,
    double CreatedAt,
    double UpdatedAt,
    double ExpiresAt,
    IReadOnlyList<OrganizationInvitationRole> OrganizationRoles);

// Used both as the resend-message request body and as a helper to build
// the MessagePayload field in CreateOrganizationInvitationRequest.
public record OrganizationInvitationMessagePayload(
    string? Code,
    string? Link,
    string? Locale,
    string? UiLocales);

// MessagePayload is JsonElement to support the oneOf(object | false) schema:
//   JsonSerializer.SerializeToElement(false)                  → don't send email
//   JsonSerializer.SerializeToElement(new OrganizationInvitationMessagePayload(...))  → send email
public record CreateOrganizationInvitationRequest(
    string Invitee,
    string OrganizationId,
    double ExpiresAt,
    JsonElement MessagePayload,
    string? InviterId = null,
    IReadOnlyList<string>? OrganizationRoleIds = null);

public record ReplaceOrganizationInvitationStatusRequest(
    OrganizationInvitationStatus Status,
    string? AcceptedUserId = null);

// Organization Roles

public record OrganizationRoleScope(string Id, string Name);

public record OrganizationRoleResourceScopeResource(string Id, string Name);

public record OrganizationRoleResourceScope(
    string Id,
    string Name,
    string TenantId,
    string ResourceId,
    string? Description,
    double CreatedAt,
    OrganizationRoleResourceScopeResource? Resource);

public record OrganizationRoleWithScopes(
    string TenantId,
    string Id,
    string Name,
    string? Description,
    OrganizationRoleType Type,
    IReadOnlyList<OrganizationRoleScope> Scopes,
    IReadOnlyList<OrganizationRoleResourceScope> ResourceScopes);

public record CreateOrganizationRoleRequest(
    string Name,
    IReadOnlyList<string> OrganizationScopeIds,
    IReadOnlyList<string> ResourceScopeIds,
    string? Description = null,
    OrganizationRoleType? Type = null);

public record UpdateOrganizationRoleRequest(
    string? Name = null,
    string? Description = null,
    OrganizationRoleType? Type = null);

public record OrganizationScopeIdsRequest(IReadOnlyList<string> OrganizationScopeIds);

public record ResourceScopeIdsRequest(IReadOnlyList<string> ScopeIds);

// Organization Scopes

public record OrganizationScope(string TenantId, string Id, string Name, string? Description);

public record CreateOrganizationScopeRequest(string Name, string? Description = null);

public record UpdateOrganizationScopeRequest(string? Name = null, string? Description = null);
