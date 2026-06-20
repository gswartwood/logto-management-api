using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using Logto.ManagementApi.Utils;

namespace Logto.ManagementApi.Verifications;

public sealed class VerificationsClient(HttpClient httpClient) : IVerificationsClient
{
    private static readonly JsonSerializerOptions WriteOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
    };

    public async Task<VerificationRecord> CreateByPasswordAsync(CreatePasswordVerificationRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync("verifications/password", request, WriteOptions, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<VerificationRecord>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for verifications/password.");
    }

    public async Task<VerificationRecord> CreateByVerificationCodeAsync(CreateVerificationCodeVerificationRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync("verifications/verification-code", request, WriteOptions, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<VerificationRecord>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for verifications/verification-code.");
    }

    public async Task<VerificationRecordResult> VerifyVerificationCodeAsync(VerifyVerificationCodeVerificationRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync("verifications/verification-code/verify", request, WriteOptions, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<VerificationRecordResult>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for verifications/verification-code/verify.");
    }

    public async Task<SocialVerificationResult> CreateBySocialAsync(CreateSocialVerificationRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync("verifications/social", request, WriteOptions, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<SocialVerificationResult>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for verifications/social.");
    }

    public async Task<VerificationRecordResult> VerifySocialAsync(VerifySocialVerificationRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync("verifications/social/verify", request, WriteOptions, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<VerificationRecordResult>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for verifications/social/verify.");
    }

    public async Task<WebAuthnRegistrationResult> GenerateWebAuthnRegistrationOptionsAsync(CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsync("verifications/web-authn/registration", null, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<WebAuthnRegistrationResult>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for verifications/web-authn/registration.");
    }

    public async Task<VerificationRecordResult> VerifyWebAuthnRegistrationAsync(VerifyWebAuthnRegistrationRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync("verifications/web-authn/registration/verify", request, WriteOptions, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<VerificationRecordResult>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for verifications/web-authn/registration/verify.");
    }
}
