namespace Logto.ManagementApi.Configs;

public interface IConfigsClient
{
    Task<AdminConsoleConfig> GetAdminConsoleConfigAsync(CancellationToken cancellationToken = default);
    Task<AdminConsoleConfig> UpdateAdminConsoleConfigAsync(UpdateAdminConsoleConfigRequest request, CancellationToken cancellationToken = default);

    Task<OidcSessionConfig> GetOidcSessionConfigAsync(CancellationToken cancellationToken = default);
    Task<OidcSessionConfig> UpdateOidcSessionConfigAsync(UpdateOidcSessionConfigRequest request, CancellationToken cancellationToken = default);

    Task<IEnumerable<OidcKey>> ListOidcKeysAsync(OidcKeyType keyType, CancellationToken cancellationToken = default);
    Task DeleteOidcKeyAsync(OidcKeyType keyType, string keyId, CancellationToken cancellationToken = default);
    Task<IEnumerable<OidcKey>> RotateOidcKeysAsync(OidcKeyType keyType, RotateOidcKeysRequest request, CancellationToken cancellationToken = default);

    Task<IEnumerable<JwtCustomizer>> ListJwtCustomizersAsync(CancellationToken cancellationToken = default);
    Task<JwtCustomizer> GetJwtCustomizerAsync(JwtCustomizerTokenType tokenType, CancellationToken cancellationToken = default);
    Task<JwtCustomizer> UpsertJwtCustomizerAsync(JwtCustomizerTokenType tokenType, UpsertJwtCustomizerRequest request, CancellationToken cancellationToken = default);
    Task<JwtCustomizer> UpdateJwtCustomizerAsync(JwtCustomizerTokenType tokenType, UpsertJwtCustomizerRequest request, CancellationToken cancellationToken = default);
    Task DeleteJwtCustomizerAsync(JwtCustomizerTokenType tokenType, CancellationToken cancellationToken = default);
    Task<Dictionary<string, object>> TestJwtCustomizerAsync(TestJwtCustomizerRequest request, CancellationToken cancellationToken = default);

    Task<IdTokenConfig> GetIdTokenConfigAsync(CancellationToken cancellationToken = default);
    Task<IdTokenConfig> UpsertIdTokenConfigAsync(UpsertIdTokenConfigRequest request, CancellationToken cancellationToken = default);
}
