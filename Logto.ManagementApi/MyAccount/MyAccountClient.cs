using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using Logto.ManagementApi.Utils;

namespace Logto.ManagementApi.MyAccount;

public sealed class MyAccountClient(HttpClient httpClient) : IMyAccountClient
{
    private static readonly JsonSerializerOptions WriteOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
    };

    public async Task<MyAccountUser> GetAsync(CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync("my-account", cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<MyAccountUser>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for my-account get.");
    }

    public async Task<MyAccountUser> UpdateAsync(UpdateMyAccountRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PatchAsJsonAsync("my-account", request, WriteOptions, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<MyAccountUser>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for my-account update.");
    }

    public async Task<MyAccountProfile> UpdateProfileAsync(UpdateMyAccountProfileRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PatchAsJsonAsync("my-account/profile", request, WriteOptions, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<MyAccountProfile>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for my-account profile update.");
    }

    public async Task UpdatePasswordAsync(UpdateMyAccountPasswordRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync("my-account/password", request, WriteOptions, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
    }

    public async Task<MyAccountMfaSettings> GetMfaSettingsAsync(CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync("my-account/mfa-settings", cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<MyAccountMfaSettings>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for my-account mfa-settings get.");
    }

    public async Task<MyAccountMfaSettings> UpdateMfaSettingsAsync(UpdateMyAccountMfaSettingsRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PatchAsJsonAsync("my-account/mfa-settings", request, WriteOptions, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<MyAccountMfaSettings>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for my-account mfa-settings update.");
    }

    public async Task<MyAccountLogtoConfigs> GetLogtoConfigsAsync(CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync("my-account/logto-configs", cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<MyAccountLogtoConfigs>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for my-account logto-configs get.");
    }

    public async Task<MyAccountLogtoConfigs> UpdateLogtoConfigsAsync(UpdateMyAccountLogtoConfigsRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PatchAsJsonAsync("my-account/logto-configs", request, WriteOptions, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<MyAccountLogtoConfigs>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for my-account logto-configs update.");
    }

    public async Task<MyAccountAccessToken> GetIdentityAccessTokenAsync(string target, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync($"my-account/identities/{Uri.EscapeDataString(target)}/access-token", cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<MyAccountAccessToken>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for identity access-token get.");
    }

    public async Task<MyAccountAccessToken> UpdateIdentityAccessTokenAsync(string target, UpdateIdentityAccessTokenRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PutAsJsonAsync($"my-account/identities/{Uri.EscapeDataString(target)}/access-token", request, WriteOptions, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<MyAccountAccessToken>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for identity access-token update.");
    }

    public async Task<MyAccountAccessToken> GetSsoIdentityAccessTokenAsync(string connectorId, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync($"my-account/sso-identities/{Uri.EscapeDataString(connectorId)}/access-token", cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<MyAccountAccessToken>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for sso-identity access-token get.");
    }

    public async Task UpdatePrimaryEmailAsync(UpdatePrimaryEmailRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync("my-account/primary-email", request, WriteOptions, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
    }

    public async Task DeletePrimaryEmailAsync(CancellationToken cancellationToken = default)
    {
        var response = await httpClient.DeleteAsync("my-account/primary-email", cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
    }

    public async Task UpdatePrimaryPhoneAsync(UpdatePrimaryPhoneRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync("my-account/primary-phone", request, WriteOptions, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
    }

    public async Task DeletePrimaryPhoneAsync(CancellationToken cancellationToken = default)
    {
        var response = await httpClient.DeleteAsync("my-account/primary-phone", cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
    }

    public async Task AddIdentityAsync(AddIdentityRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync("my-account/identities", request, WriteOptions, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
    }

    public async Task ReplaceIdentityAsync(AddIdentityRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PutAsJsonAsync("my-account/identities", request, WriteOptions, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
    }

    public async Task DeleteIdentityAsync(string target, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.DeleteAsync($"my-account/identities/{Uri.EscapeDataString(target)}", cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<MyAccountMfaVerification>> ListMfaVerificationsAsync(CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync("my-account/mfa-verifications", cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<IReadOnlyList<MyAccountMfaVerification>>(cancellationToken: cancellationToken) ?? [];
    }

    public async Task CreateMfaVerificationAsync(CreateMfaVerificationRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync("my-account/mfa-verifications", request, WriteOptions, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
    }

    public async Task BindTotpAsync(BindTotpRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PutAsJsonAsync("my-account/mfa-verifications/totp", request, WriteOptions, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
    }

    public async Task<JsonElement?> GenerateTotpSecretAsync(CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsync("my-account/mfa-verifications/totp-secret/generate", null, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<JsonElement?>(cancellationToken: cancellationToken);
    }

    public async Task<JsonElement?> GenerateBackupCodesAsync(CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsync("my-account/mfa-verifications/backup-codes/generate", null, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<JsonElement?>(cancellationToken: cancellationToken);
    }

    public async Task<JsonElement?> GetBackupCodesAsync(CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync("my-account/mfa-verifications/backup-codes", cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<JsonElement?>(cancellationToken: cancellationToken);
    }

    public async Task UpdateMfaVerificationNameAsync(string verificationId, UpdateMfaVerificationNameRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PatchAsJsonAsync($"my-account/mfa-verifications/{Uri.EscapeDataString(verificationId)}/name", request, WriteOptions, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
    }

    public async Task DeleteMfaVerificationAsync(string verificationId, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.DeleteAsync($"my-account/mfa-verifications/{Uri.EscapeDataString(verificationId)}", cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
    }

    public async Task<MyAccountSessionsResult> ListSessionsAsync(CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync("my-account/sessions", cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<MyAccountSessionsResult>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for my-account sessions list.");
    }

    public async Task DeleteSessionAsync(string sessionId, RevokeGrantsTarget? revokeGrantsTarget = null, CancellationToken cancellationToken = default)
    {
        var url = revokeGrantsTarget is null
            ? $"my-account/sessions/{Uri.EscapeDataString(sessionId)}"
            : $"my-account/sessions/{Uri.EscapeDataString(sessionId)}?revokeGrantsTarget={ToQueryString(revokeGrantsTarget.Value)}";
        var response = await httpClient.DeleteAsync(url, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
    }

    public async Task<MyAccountGrantsResult> ListGrantsAsync(GrantAppType? appType = null, CancellationToken cancellationToken = default)
    {
        var url = appType is null
            ? "my-account/grants"
            : $"my-account/grants?appType={ToQueryString(appType.Value)}";
        var response = await httpClient.GetAsync(url, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<MyAccountGrantsResult>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for my-account grants list.");
    }

    public async Task DeleteGrantAsync(string grantId, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.DeleteAsync($"my-account/grants/{Uri.EscapeDataString(grantId)}", cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
    }

    public async Task<AvatarUploadResult> UploadAvatarAsync(Stream fileStream, string fileName, string contentType, CancellationToken cancellationToken = default)
    {
        using var content = new MultipartFormDataContent();
        using var streamContent = new StreamContent(fileStream);
        streamContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(contentType);
        content.Add(streamContent, "avatar", fileName);

        var response = await httpClient.PostAsync("my-account/user-assets/avatar", content, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<AvatarUploadResult>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for avatar upload.");
    }

    private static string ToQueryString(GrantAppType appType) => appType switch
    {
        GrantAppType.FirstParty => "firstParty",
        GrantAppType.ThirdParty => "thirdParty",
        _ => throw new ArgumentOutOfRangeException(nameof(appType)),
    };

    private static string ToQueryString(RevokeGrantsTarget target) => target switch
    {
        RevokeGrantsTarget.All => "all",
        RevokeGrantsTarget.FirstParty => "firstParty",
        _ => throw new ArgumentOutOfRangeException(nameof(target)),
    };
}
