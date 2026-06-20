using System.Text.Json;
using System.Text.Json.Serialization;
using Logto.ManagementApi.Applications;

namespace Logto.ManagementApi.Saml;

[JsonConverter(typeof(JsonStringEnumConverter<SamlBindingType>))]
public enum SamlBindingType
{
    [JsonStringEnumMemberName("urn:oasis:names:tc:SAML:2.0:bindings:HTTP-POST")] HttpPost,
    [JsonStringEnumMemberName("urn:oasis:names:tc:SAML:2.0:bindings:HTTP-Redirect")] HttpRedirect,
}

[JsonConverter(typeof(JsonStringEnumConverter<SamlNameIdFormat>))]
public enum SamlNameIdFormat
{
    [JsonStringEnumMemberName("urn:oasis:names:tc:SAML:2.0:nameid-format:persistent")] Persistent,
    [JsonStringEnumMemberName("urn:oasis:names:tc:SAML:1.1:nameid-format:emailAddress")] EmailAddress,
    [JsonStringEnumMemberName("urn:oasis:names:tc:SAML:2.0:nameid-format:transient")] Transient,
    [JsonStringEnumMemberName("urn:oasis:names:tc:SAML:1.1:nameid-format:unspecified")] Unspecified,
}

public record SamlAcsUrl(SamlBindingType Binding, string Url);

public record SamlApplication
{
    public required string TenantId { get; init; }
    public required string Id { get; init; }
    public required string Name { get; init; }
    public string? Description { get; init; }
    public ApplicationType Type { get; init; }
    public Dictionary<string, object> CustomData { get; init; } = [];
    public bool IsThirdParty { get; init; }
    public bool AppLevelAccessControlEnabled { get; init; }
    public double CreatedAt { get; init; }
    public Dictionary<string, string?> AttributeMapping { get; init; } = [];
    public string? EntityId { get; init; }
    public SamlAcsUrl? AcsUrl { get; init; }
    public Dictionary<string, object>? Encryption { get; init; }
    public SamlNameIdFormat NameIdFormat { get; init; }
}

public record CreateSamlApplicationRequest(
    string Name,
    SamlNameIdFormat NameIdFormat,
    string? Description = null,
    Dictionary<string, object>? CustomData = null,
    Dictionary<string, string?>? AttributeMapping = null,
    string? EntityId = null,
    SamlAcsUrl? AcsUrl = null,
    Dictionary<string, object>? Encryption = null);

public record UpdateSamlApplicationRequest(
    string? Name = null,
    string? Description = null,
    Dictionary<string, object>? CustomData = null,
    bool? AppLevelAccessControlEnabled = null,
    Dictionary<string, string?>? AttributeMapping = null,
    string? EntityId = null,
    SamlAcsUrl? AcsUrl = null,
    Dictionary<string, object>? Encryption = null,
    SamlNameIdFormat? NameIdFormat = null);

public record SamlFingerprintValue(string Formatted, string Unformatted);

public record SamlCertificateFingerprints(SamlFingerprintValue Sha256);

// expiresAt is number in create response but date-time string in list response
public record SamlApplicationSecret(
    string Id,
    string Certificate,
    double CreatedAt,
    JsonElement ExpiresAt,
    bool Active,
    SamlCertificateFingerprints Fingerprints,
    string? Fingerprint = null,
    bool? IsActive = null);

public record CreateSamlApplicationSecretRequest(int LifeSpanInYears);

public record UpdateSamlApplicationSecretRequest(bool Active);

// Property names must be sent as-is (not camelCased) — use SamlAuthnWriteOptions in client
public record CreateSamlAuthnRequest(string SAMLRequest, string? RelayState = null);
