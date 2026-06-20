using System.Text.Json.Serialization;
using Logto.ManagementApi.Applications;

namespace Logto.ManagementApi.Roles;

[JsonConverter(typeof(JsonStringEnumConverter<RoleType>))]
public enum RoleType { User, MachineToMachine }

public record Role(
    string TenantId,
    string Id,
    string Name,
    string Description,
    RoleType Type,
    bool IsDefault);

public record RoleFeaturedUser(string Id, string? Avatar, string? Name);

public record RoleFeaturedApplication(string Id, string Name, ApplicationType Type);

public record RoleWithStats(
    string TenantId,
    string Id,
    string Name,
    string Description,
    RoleType Type,
    bool IsDefault,
    double UsersCount,
    IReadOnlyList<RoleFeaturedUser> FeaturedUsers,
    double ApplicationsCount,
    IReadOnlyList<RoleFeaturedApplication> FeaturedApplications);

public record ListRolesRequest(
    string? ExcludeUserId = null,
    string? ExcludeApplicationId = null,
    RoleType? Type = null,
    int? Page = null,
    int? PageSize = null,
    Dictionary<string, string>? SearchParams = null);

public record CreateRoleRequest(
    string Name,
    string Description,
    RoleType? Type = null,
    bool? IsDefault = null,
    IReadOnlyList<string>? ScopeIds = null);

public record UpdateRoleRequest(
    string? Name = null,
    string? Description = null,
    bool? IsDefault = null);

public record RoleScopeResource(
    string TenantId,
    string Id,
    string Name,
    string Indicator,
    bool IsDefault,
    double AccessTokenTtl);

public record RoleScope(
    string TenantId,
    string Id,
    string ResourceId,
    string Name,
    string? Description,
    double CreatedAt,
    RoleScopeResource Resource);

public record AssignRoleApplicationsRequest(IReadOnlyList<string> ApplicationIds);

public record AssignRoleScopesRequest(IReadOnlyList<string> ScopeIds);

public record AssignRoleUsersRequest(IReadOnlyList<string> UserIds);

public record RoleUserIdentity(string UserId, Dictionary<string, object>? Details = null);

public record UserAddress(
    string? Formatted = null,
    string? StreetAddress = null,
    string? Locality = null,
    string? Region = null,
    string? PostalCode = null,
    string? Country = null);

public record RoleUserProfile(
    string? FamilyName = null,
    string? GivenName = null,
    string? MiddleName = null,
    string? Nickname = null,
    string? PreferredUsername = null,
    string? Profile = null,
    string? Website = null,
    string? Gender = null,
    string? Birthdate = null,
    string? Zoneinfo = null,
    string? Locale = null,
    UserAddress? Address = null);

public record RoleUserSsoIdentity(
    string TenantId,
    string Id,
    string UserId,
    string Issuer,
    string IdentityId,
    Dictionary<string, object> Detail,
    double CreatedAt,
    double UpdatedAt,
    string SsoConnectorId);

public record RoleUser(
    string Id,
    string? Username,
    string? PrimaryEmail,
    string? PrimaryPhone,
    string? Name,
    string? Avatar,
    Dictionary<string, object> CustomData,
    Dictionary<string, RoleUserIdentity> Identities,
    double? LastSignInAt,
    double CreatedAt,
    double UpdatedAt,
    RoleUserProfile Profile,
    string? ApplicationId,
    bool IsSuspended,
    bool? HasPassword = null,
    bool? HasSecurityVerificationMethod = null,
    IReadOnlyList<RoleUserSsoIdentity>? SsoIdentities = null);
