using System.Text.Json;
using System.Text.Json.Serialization;

namespace Logto.ManagementApi.Configs;

public record AdminConsoleConfig
{
    public required bool SignInExperienceCustomized { get; init; }
    public required bool OrganizationCreated { get; init; }
    public DevelopmentTenantMigrationNotification? DevelopmentTenantMigrationNotification { get; init; }
    public CheckedChargeNotification? CheckedChargeNotification { get; init; }
}

public record DevelopmentTenantMigrationNotification
{
    public required bool IsPaidTenant { get; init; }
    public required string Tag { get; init; }
    public double? ReadAt { get; init; }
}

public record CheckedChargeNotification
{
    public bool? Token { get; init; }
    public bool? ApiResource { get; init; }
    public bool? MachineToMachineApp { get; init; }
    public bool? TenantMember { get; init; }
}

public record UpdateAdminConsoleConfigRequest
{
    public bool? SignInExperienceCustomized { get; init; }
    public bool? OrganizationCreated { get; init; }
    public DevelopmentTenantMigrationNotification? DevelopmentTenantMigrationNotification { get; init; }
    public CheckedChargeNotification? CheckedChargeNotification { get; init; }
}

public record OidcSessionConfig
{
    public required double Ttl { get; init; }
}

public record UpdateOidcSessionConfigRequest
{
    public double? Ttl { get; init; }
}

public enum OidcKeyType { PrivateKeys, CookieKeys }

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum OidcSigningKeyAlgorithm { RSA, EC }

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum OidcKeyStatus { Next, Current, Previous }

public record OidcKey
{
    public required string Id { get; init; }
    public required double CreatedAt { get; init; }
    public OidcSigningKeyAlgorithm? SigningKeyAlgorithm { get; init; }
    public OidcKeyStatus? Status { get; init; }
    public double? EffectiveAt { get; init; }
}

public record RotateOidcKeysRequest
{
    public OidcSigningKeyAlgorithm? SigningKeyAlgorithm { get; init; }
    public double? RotationGracePeriod { get; init; }
}

public enum JwtCustomizerTokenType { AccessToken, ClientCredentials }

public record JwtCustomizer
{
    public required string Script { get; init; }
    public Dictionary<string, string>? EnvironmentVariables { get; init; }
    public JsonElement? ContextSample { get; init; }
    public JsonElement? TokenSample { get; init; }
    public bool? BlockIssuanceOnError { get; init; }
}

public record UpsertJwtCustomizerRequest
{
    public string? Script { get; init; }
    public Dictionary<string, string>? EnvironmentVariables { get; init; }
    public JsonElement? ContextSample { get; init; }
    public JsonElement? TokenSample { get; init; }
    public bool? BlockIssuanceOnError { get; init; }
}

public record TestJwtCustomizerRequest
{
    public required JwtCustomizerTokenType TokenType { get; init; }
    public JsonElement? Payload { get; init; }
}

public record IdTokenConfig
{
    public List<string>? EnabledExtendedClaims { get; init; }
}

public record UpsertIdTokenConfigRequest
{
    public List<string>? EnabledExtendedClaims { get; init; }
}
