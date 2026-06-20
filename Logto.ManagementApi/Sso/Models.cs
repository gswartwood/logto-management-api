using System.Text.Json.Serialization;

namespace Logto.ManagementApi.Sso;

[JsonConverter(typeof(JsonStringEnumConverter<SsoProviderName>))]
public enum SsoProviderName
{
    [JsonStringEnumMemberName("OIDC")] Oidc,
    [JsonStringEnumMemberName("SAML")] Saml,
    [JsonStringEnumMemberName("AzureAD")] AzureAd,
    GoogleWorkspace,
    Okta,
    AzureAdOidc,
}

[JsonConverter(typeof(JsonStringEnumConverter<SsoProviderType>))]
public enum SsoProviderType
{
    [JsonStringEnumMemberName("oidc")] Oidc,
    [JsonStringEnumMemberName("saml")] Saml,
}

public record SsoConnectorBranding(
    string? DisplayName,
    string? Logo,
    string? DarkLogo
);

public record SsoConnectorProvider(
    SsoProviderName ProviderName,
    SsoProviderType ProviderType,
    string Logo,
    string LogoDark,
    string Description,
    string Name
);

public record SsoConnector(
    string TenantId,
    string Id,
    SsoProviderName ProviderName,
    string ConnectorName,
    Dictionary<string, object> Config,
    IReadOnlyList<string> Domains,
    SsoConnectorBranding Branding,
    bool SyncProfile,
    bool EnableTokenStorage,
    double CreatedAt,
    string? Name,
    SsoProviderType? ProviderType,
    string? ProviderLogo,
    string? ProviderLogoDark,
    Dictionary<string, object>? ProviderConfig
);

public record CreateSsoConnectorRequest(
    string ProviderName,
    string ConnectorName,
    Dictionary<string, object>? Config = null,
    IReadOnlyList<string>? Domains = null,
    SsoConnectorBranding? Branding = null,
    bool? SyncProfile = null,
    bool? EnableTokenStorage = null
);

public record UpdateSsoConnectorRequest(
    string? ConnectorName = null,
    Dictionary<string, object>? Config = null,
    IReadOnlyList<string>? Domains = null,
    SsoConnectorBranding? Branding = null,
    bool? SyncProfile = null,
    bool? EnableTokenStorage = null
);
