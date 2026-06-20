using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using Logto.ManagementApi.Utils;

namespace Logto.ManagementApi.Configs;

public sealed class ConfigsClient(HttpClient httpClient) : IConfigsClient
{
    private static readonly JsonSerializerOptions WriteOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
    };

    public async Task<AdminConsoleConfig> GetAdminConsoleConfigAsync(CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync("configs/admin-console", cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<AdminConsoleConfig>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for admin console config get.");
    }

    public async Task<AdminConsoleConfig> UpdateAdminConsoleConfigAsync(UpdateAdminConsoleConfigRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PatchAsJsonAsync("configs/admin-console", request, WriteOptions, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<AdminConsoleConfig>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for admin console config update.");
    }

    public async Task<OidcSessionConfig> GetOidcSessionConfigAsync(CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync("configs/oidc/session", cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<OidcSessionConfig>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for OIDC session config get.");
    }

    public async Task<OidcSessionConfig> UpdateOidcSessionConfigAsync(UpdateOidcSessionConfigRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PatchAsJsonAsync("configs/oidc/session", request, WriteOptions, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<OidcSessionConfig>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for OIDC session config update.");
    }

    public async Task<IEnumerable<OidcKey>> ListOidcKeysAsync(OidcKeyType keyType, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync($"configs/oidc/{ToPathString(keyType)}", cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<IEnumerable<OidcKey>>(cancellationToken: cancellationToken) ?? [];
    }

    public async Task DeleteOidcKeyAsync(OidcKeyType keyType, string keyId, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.DeleteAsync($"configs/oidc/{ToPathString(keyType)}/{Uri.EscapeDataString(keyId)}", cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
    }

    public async Task<IEnumerable<OidcKey>> RotateOidcKeysAsync(OidcKeyType keyType, RotateOidcKeysRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync($"configs/oidc/{ToPathString(keyType)}/rotate", request, WriteOptions, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<IEnumerable<OidcKey>>(cancellationToken: cancellationToken) ?? [];
    }

    public async Task<IEnumerable<JwtCustomizer>> ListJwtCustomizersAsync(CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync("configs/jwt-customizer", cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<IEnumerable<JwtCustomizer>>(cancellationToken: cancellationToken) ?? [];
    }

    public async Task<JwtCustomizer> GetJwtCustomizerAsync(JwtCustomizerTokenType tokenType, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync($"configs/jwt-customizer/{ToPathString(tokenType)}", cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<JwtCustomizer>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for JWT customizer get.");
    }

    public async Task<JwtCustomizer> UpsertJwtCustomizerAsync(JwtCustomizerTokenType tokenType, UpsertJwtCustomizerRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PutAsJsonAsync($"configs/jwt-customizer/{ToPathString(tokenType)}", request, WriteOptions, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<JwtCustomizer>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for JWT customizer upsert.");
    }

    public async Task<JwtCustomizer> UpdateJwtCustomizerAsync(JwtCustomizerTokenType tokenType, UpsertJwtCustomizerRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PatchAsJsonAsync($"configs/jwt-customizer/{ToPathString(tokenType)}", request, WriteOptions, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<JwtCustomizer>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for JWT customizer update.");
    }

    public async Task DeleteJwtCustomizerAsync(JwtCustomizerTokenType tokenType, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.DeleteAsync($"configs/jwt-customizer/{ToPathString(tokenType)}", cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
    }

    public async Task<Dictionary<string, object>> TestJwtCustomizerAsync(TestJwtCustomizerRequest request, CancellationToken cancellationToken = default)
    {
        var body = new { tokenType = ToPathString(request.TokenType), payload = request.Payload };
        var response = await httpClient.PostAsJsonAsync("configs/jwt-customizer/test", body, WriteOptions, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<Dictionary<string, object>>(cancellationToken: cancellationToken) ?? [];
    }

    public async Task<IdTokenConfig> GetIdTokenConfigAsync(CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync("configs/id-token", cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<IdTokenConfig>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for ID token config get.");
    }

    public async Task<IdTokenConfig> UpsertIdTokenConfigAsync(UpsertIdTokenConfigRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PutAsJsonAsync("configs/id-token", request, WriteOptions, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<IdTokenConfig>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for ID token config upsert.");
    }

    private static string ToPathString(OidcKeyType keyType) => keyType switch
    {
        OidcKeyType.PrivateKeys => "private-keys",
        OidcKeyType.CookieKeys => "cookie-keys",
        _ => throw new ArgumentOutOfRangeException(nameof(keyType)),
    };

    private static string ToPathString(JwtCustomizerTokenType tokenType) => tokenType switch
    {
        JwtCustomizerTokenType.AccessToken => "access-token",
        JwtCustomizerTokenType.ClientCredentials => "client-credentials",
        _ => throw new ArgumentOutOfRangeException(nameof(tokenType)),
    };
}
