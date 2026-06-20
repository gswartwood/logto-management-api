using System.Text.Json;
using System.Text.Json.Serialization;

namespace Logto.ManagementApi.MyAccount;

public record MyAccountAddress(
    string? Formatted, string? StreetAddress, string? Locality,
    string? Region, string? PostalCode, string? Country);

public record MyAccountProfile(
    string? FamilyName, string? GivenName, string? MiddleName, string? Nickname,
    string? PreferredUsername, string? Profile, string? Website, string? Gender,
    string? Birthdate, string? Zoneinfo, string? Locale, MyAccountAddress? Address);

public record MyAccountIdentityEntry(string UserId, Dictionary<string, object>? Details);

public record MyAccountSsoIdentity(
    string TenantId, string Id, string UserId, string Issuer, string IdentityId,
    Dictionary<string, object> Detail, double CreatedAt, double UpdatedAt, string SsoConnectorId);

public record MyAccountUser(
    string? Id, string? Username, string? PrimaryEmail, string? PrimaryPhone,
    string? Name, string? Avatar, Dictionary<string, object>? CustomData,
    Dictionary<string, MyAccountIdentityEntry>? Identities,
    double? LastSignInAt, double? CreatedAt, double? UpdatedAt,
    MyAccountProfile? Profile, string? ApplicationId,
    bool? IsSuspended, bool? HasPassword, bool? HasSecurityVerificationMethod,
    IReadOnlyList<MyAccountSsoIdentity>? SsoIdentities);

public record UpdateMyAccountRequest(
    string? Name = null,
    string? Avatar = null,
    string? Username = null,
    Dictionary<string, object>? CustomData = null);

public record UpdateMyAccountProfileRequest(
    string? FamilyName = null, string? GivenName = null, string? MiddleName = null,
    string? Nickname = null, string? PreferredUsername = null, string? Profile = null,
    string? Website = null, string? Gender = null, string? Birthdate = null,
    string? Zoneinfo = null, string? Locale = null, MyAccountAddress? Address = null);

public record UpdateMyAccountPasswordRequest(string Password);

public record MyAccountMfaSettings(bool SkipMfaOnSignIn);
public record UpdateMyAccountMfaSettingsRequest(bool SkipMfaOnSignIn);

public record MyAccountMfaLogtoConfig(bool Skipped, bool SkipMfaOnSignIn, bool? Enabled = null);
public record MyAccountPasskeySignInLogtoConfig(bool Skipped);
public record MyAccountLogtoConfigs(MyAccountMfaLogtoConfig Mfa, MyAccountPasskeySignInLogtoConfig PasskeySignIn);

public record UpdateMfaLogtoConfig(
    bool? Enabled = null,
    bool? Skipped = null,
    bool? AdditionalBindingSuggestionSkipped = null,
    bool? SkipMfaOnSignIn = null);

public record UpdatePasskeySignInLogtoConfig(bool? Skipped = null);

public record UpdateMyAccountLogtoConfigsRequest(
    UpdateMfaLogtoConfig? Mfa = null,
    UpdatePasskeySignInLogtoConfig? PasskeySignIn = null);

public record MyAccountAccessToken
{
    [JsonPropertyName("access_token")]
    public required string AccessToken { get; init; }

    [JsonPropertyName("scope")]
    public string? Scope { get; init; }

    [JsonPropertyName("token_type")]
    public string? TokenType { get; init; }

    [JsonPropertyName("expires_in")]
    public JsonElement? ExpiresIn { get; init; }
}

public record UpdateIdentityAccessTokenRequest(string VerificationRecordId);

public record UpdatePrimaryEmailRequest(string Email, string NewIdentifierVerificationRecordId);
public record UpdatePrimaryPhoneRequest(string Phone, string NewIdentifierVerificationRecordId);
public record AddIdentityRequest(string NewIdentifierVerificationRecordId);

[JsonConverter(typeof(JsonStringEnumConverter<MyAccountMfaVerificationType>))]
public enum MyAccountMfaVerificationType
{
    Totp,
    WebAuthn,
    BackupCode,
    EmailVerificationCode,
    PhoneVerificationCode,
}

public record MyAccountMfaVerification(
    string Id, string CreatedAt, string? LastUsedAt,
    MyAccountMfaVerificationType Type, string? Agent, string? Name, double? RemainCodes);

public record CreateMfaVerificationRequest(
    string Type,
    string? NewIdentifierVerificationRecordId = null,
    string? Name = null,
    string? Secret = null,
    string? Code = null,
    IReadOnlyList<string>? Codes = null);

public record BindTotpRequest(string Secret, string Code);
public record UpdateMfaVerificationNameRequest(string Name);

public record MyAccountSessionPayload(
    double Exp, double Iat, string Jti, string Uid, string Kind,
    double LoginTs, string AccountId, JsonElement? Authorizations);

[JsonConverter(typeof(JsonStringEnumConverter<SessionInteractionEvent>))]
public enum SessionInteractionEvent { SignIn, Register, ForgotPassword }

public record MyAccountSessionLastSubmission(
    SessionInteractionEvent InteractionEvent,
    string UserId,
    JsonElement? VerificationRecords,
    Dictionary<string, string>? SignInContext);

public record MyAccountSession(
    MyAccountSessionPayload Payload,
    MyAccountSessionLastSubmission? LastSubmission,
    string? ClientId,
    string? AccountId,
    double ExpiresAt,
    bool IsCurrent);

public record MyAccountSessionsResult(IReadOnlyList<MyAccountSession> Sessions);

public record MyAccountGrantPayload(
    double Exp, double Iat, string Jti, string Kind, string ClientId, string AccountId);

public record MyAccountGrantApplication(string Id, string Name);

public record MyAccountGrant(
    string Id, MyAccountGrantPayload Payload, double ExpiresAt, MyAccountGrantApplication Application);

public record MyAccountGrantsResult(IReadOnlyList<MyAccountGrant> Grants);

public enum GrantAppType
{
    [JsonStringEnumMemberName("firstParty")] FirstParty,
    [JsonStringEnumMemberName("thirdParty")] ThirdParty,
}

public enum RevokeGrantsTarget
{
    [JsonStringEnumMemberName("all")] All,
    [JsonStringEnumMemberName("firstParty")] FirstParty,
}

public record AvatarUploadResult(string Url);
