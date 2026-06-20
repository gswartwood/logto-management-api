using System.Text.Json;
using System.Text.Json.Serialization;

namespace Logto.ManagementApi.Users;

// === User assets ===

[JsonConverter(typeof(JsonStringEnumConverter<UserAssetStatus>))]
public enum UserAssetStatus
{
    [JsonStringEnumMemberName("ready")] Ready,
    [JsonStringEnumMemberName("not_configured")] NotConfigured,
}

public record UserAssetServiceStatus(
    UserAssetStatus Status,
    IReadOnlyList<string>? AllowUploadMimeTypes,
    double? MaxUploadFileSize,
    bool? IsExperienceAvatarUploadEnabled
);

public record UserAsset(string Url);

// === Users ===

[JsonConverter(typeof(JsonStringEnumConverter<PasswordAlgorithm>))]
public enum PasswordAlgorithm { Argon2i, Argon2id, Argon2d, SHA1, SHA256, MD5, Bcrypt, Legacy }

[JsonConverter(typeof(JsonStringEnumConverter<UserRoleType>))]
public enum UserRoleType { User, MachineToMachine }

[JsonConverter(typeof(JsonStringEnumConverter<MfaVerificationType>))]
public enum MfaVerificationType { Totp, WebAuthn, BackupCode, EmailVerificationCode, PhoneVerificationCode }

public record UserAddress(
    string? Formatted,
    string? StreetAddress,
    string? Locality,
    string? Region,
    string? PostalCode,
    string? Country
);

public record UserProfile(
    string? FamilyName,
    string? GivenName,
    string? MiddleName,
    string? Nickname,
    string? PreferredUsername,
    string? Profile,
    string? Website,
    string? Gender,
    string? Birthdate,
    string? Zoneinfo,
    string? Locale,
    UserAddress? Address
);

public record SocialIdentityEntry(string UserId, Dictionary<string, object>? Details);

public record SsoIdentityEntry(
    string TenantId,
    string Id,
    string UserId,
    string Issuer,
    string IdentityId,
    Dictionary<string, object> Detail,
    double CreatedAt,
    double UpdatedAt,
    string SsoConnectorId
);

public record User(
    string Id,
    string? Username,
    string? PrimaryEmail,
    string? PrimaryPhone,
    string? Name,
    string? Avatar,
    Dictionary<string, object> CustomData,
    Dictionary<string, SocialIdentityEntry> Identities,
    double? LastSignInAt,
    double CreatedAt,
    double UpdatedAt,
    UserProfile Profile,
    string? ApplicationId,
    bool IsSuspended,
    bool? HasPassword,
    bool? HasSecurityVerificationMethod,
    IReadOnlyList<SsoIdentityEntry>? SsoIdentities,
    string? PasswordDigest,
    PasswordAlgorithm? PasswordAlgorithm
);

public record CreateUserRequest(
    string? PrimaryPhone = null,
    string? PrimaryEmail = null,
    string? Username = null,
    string? Password = null,
    string? PasswordDigest = null,
    PasswordAlgorithm? PasswordAlgorithm = null,
    string? Name = null,
    string? Avatar = null,
    Dictionary<string, object>? CustomData = null,
    UserProfile? Profile = null
);

public record UpdateUserRequest(
    string? Username = null,
    string? PrimaryEmail = null,
    string? PrimaryPhone = null,
    string? Name = null,
    string? Avatar = null,
    Dictionary<string, object>? CustomData = null,
    UserProfile? Profile = null
);

public record UpdateUserCustomDataRequest(Dictionary<string, object> CustomData);

public record UpdateUserProfileRequest(UserProfile Profile);

public record UpdateUserPasswordRequest(string Password);

public record UpdateUserPasswordExpirationRequest(bool IsExpired);

public record VerifyUserPasswordRequest(string Password);

public record UserHasPasswordResult(bool HasPassword);

public record UpdateUserIsSuspendedRequest(bool IsSuspended);

// Logto configs

public record MfaLogtoConfig(bool Skipped, bool SkipMfaOnSignIn, bool? Enabled);

public record PasskeySignInLogtoConfig(bool Skipped);

public record UserLogtoConfigs(MfaLogtoConfig Mfa, PasskeySignInLogtoConfig PasskeySignIn);

public record MfaLogtoConfigUpdate(
    bool? Enabled = null,
    bool? Skipped = null,
    bool? AdditionalBindingSuggestionSkipped = null,
    bool? SkipMfaOnSignIn = null
);

public record PasskeySignInLogtoConfigUpdate(bool? Skipped = null);

public record UpdateUserLogtoConfigsRequest(
    MfaLogtoConfigUpdate? Mfa = null,
    PasskeySignInLogtoConfigUpdate? PasskeySignIn = null
);

// Roles

public record UserRole(
    string TenantId,
    string Id,
    string Name,
    string Description,
    UserRoleType Type,
    bool IsDefault
);

public record AssignUserRolesRequest(IReadOnlyList<string> RoleIds);

public record UserRoleAssignmentResult(IReadOnlyList<string> RoleIds, IReadOnlyList<string> AddedRoleIds);

public record ReplaceUserRolesResult(IReadOnlyList<string> RoleIds);

// Identities

public record ReplaceUserIdentityRequest(string UserId, Dictionary<string, object>? Details = null);

public record CreateUserIdentityRequest(string ConnectorId, Dictionary<string, object> ConnectorData);

public record TokenSecretMetadata(bool HasRefreshToken, string? Scope, double? ExpiresAt, string? TokenType);

public record TokenSecret(
    string TenantId,
    string Id,
    string UserId,
    string Type,
    TokenSecretMetadata Metadata,
    double CreatedAt,
    double UpdatedAt,
    string ConnectorId,
    string IdentityId,
    string Target
);

public record UserIdentityDetail(SocialIdentityEntry Identity, TokenSecret? TokenSecret);

public record SsoTokenSecret(
    string TenantId,
    string Id,
    string UserId,
    string Type,
    TokenSecretMetadata Metadata,
    double CreatedAt,
    double UpdatedAt,
    string SsoConnectorId,
    string Issuer,
    string IdentityId
);

public record UserSsoIdentityDetail(SsoIdentityEntry SsoIdentity, SsoTokenSecret? TokenSecret);

public record SocialIdentityWithToken(SocialIdentityEntry Identity, string Target, TokenSecret? TokenSecret);

public record SsoIdentityWithToken(SsoIdentityEntry SsoIdentity, string SsoConnectorId, SsoTokenSecret? TokenSecret);

public record UserAllIdentities(
    IReadOnlyList<SocialIdentityWithToken> SocialIdentities,
    IReadOnlyList<SsoIdentityWithToken> SsoIdentities
);

// Organizations

public record OrganizationRoleRef(string Id, string Name);

public record OrganizationColor(string? PrimaryColor, bool? IsDarkModeEnabled, string? DarkPrimaryColor);

public record OrganizationBranding(string? LogoUrl, string? DarkLogoUrl, string? Favicon, string? DarkFavicon);

public record UserOrganization(
    string TenantId,
    string Id,
    string Name,
    string? Description,
    Dictionary<string, object> CustomData,
    bool IsMfaRequired,
    OrganizationColor Color,
    OrganizationBranding Branding,
    string? CustomCss,
    double CreatedAt,
    IReadOnlyList<OrganizationRoleRef> OrganizationRoles
);

// Grants

public record UserGrantPayload(double Exp, double Iat, string Jti, string Kind, string ClientId, string AccountId);

public record UserGrantApplication(string Id, string Name);

public record UserGrant(string Id, UserGrantPayload Payload, double ExpiresAt, UserGrantApplication Application);

public record UserGrantsResult(IReadOnlyList<UserGrant> Grants);

// MFA verifications

public record MfaVerification(
    string Id,
    string CreatedAt,
    MfaVerificationType Type,
    string? LastUsedAt,
    string? Agent,
    string? Name,
    double? RemainCodes
);

public record CreateMfaVerificationRequest(
    string Type,
    string? Secret = null,
    IReadOnlyList<string>? Codes = null
);

public record MfaVerificationCreatedResult(
    string Type,
    string? Secret = null,
    string? SecretQrCode = null,
    IReadOnlyList<string>? Codes = null
);

// Personal access tokens

public record PersonalAccessToken(
    string TenantId,
    string UserId,
    string Name,
    string Value,
    double CreatedAt,
    double? ExpiresAt
);

public record CreatePersonalAccessTokenRequest(string Name, double? ExpiresAt = null);

public record UpdatePersonalAccessTokenRequest(string Name, string? CurrentName = null);

public record DeletePersonalAccessTokenRequest(string Name);

// Sessions

public record SessionPayload(
    double Exp,
    double Iat,
    string Jti,
    string Uid,
    string Kind,
    double LoginTs,
    string AccountId,
    Dictionary<string, JsonElement>? Authorizations
);

public record UserSession(
    SessionPayload Payload,
    JsonElement? LastSubmission,
    string? ClientId,
    string? AccountId,
    double ExpiresAt
);

public record UserSessionsResult(IReadOnlyList<UserSession> Sessions);
