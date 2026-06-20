using System.Text.Json;

namespace Logto.ManagementApi.MyAccount;

public interface IMyAccountClient
{
    Task<MyAccountUser> GetAsync(CancellationToken cancellationToken = default);
    Task<MyAccountUser> UpdateAsync(UpdateMyAccountRequest request, CancellationToken cancellationToken = default);

    Task<MyAccountProfile> UpdateProfileAsync(UpdateMyAccountProfileRequest request, CancellationToken cancellationToken = default);

    Task UpdatePasswordAsync(UpdateMyAccountPasswordRequest request, CancellationToken cancellationToken = default);

    Task<MyAccountMfaSettings> GetMfaSettingsAsync(CancellationToken cancellationToken = default);
    Task<MyAccountMfaSettings> UpdateMfaSettingsAsync(UpdateMyAccountMfaSettingsRequest request, CancellationToken cancellationToken = default);

    Task<MyAccountLogtoConfigs> GetLogtoConfigsAsync(CancellationToken cancellationToken = default);
    Task<MyAccountLogtoConfigs> UpdateLogtoConfigsAsync(UpdateMyAccountLogtoConfigsRequest request, CancellationToken cancellationToken = default);

    Task<MyAccountAccessToken> GetIdentityAccessTokenAsync(string target, CancellationToken cancellationToken = default);
    Task<MyAccountAccessToken> UpdateIdentityAccessTokenAsync(string target, UpdateIdentityAccessTokenRequest request, CancellationToken cancellationToken = default);

    Task<MyAccountAccessToken> GetSsoIdentityAccessTokenAsync(string connectorId, CancellationToken cancellationToken = default);

    Task UpdatePrimaryEmailAsync(UpdatePrimaryEmailRequest request, CancellationToken cancellationToken = default);
    Task DeletePrimaryEmailAsync(CancellationToken cancellationToken = default);

    Task UpdatePrimaryPhoneAsync(UpdatePrimaryPhoneRequest request, CancellationToken cancellationToken = default);
    Task DeletePrimaryPhoneAsync(CancellationToken cancellationToken = default);

    Task AddIdentityAsync(AddIdentityRequest request, CancellationToken cancellationToken = default);
    Task ReplaceIdentityAsync(AddIdentityRequest request, CancellationToken cancellationToken = default);
    Task DeleteIdentityAsync(string target, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<MyAccountMfaVerification>> ListMfaVerificationsAsync(CancellationToken cancellationToken = default);
    Task CreateMfaVerificationAsync(CreateMfaVerificationRequest request, CancellationToken cancellationToken = default);
    Task BindTotpAsync(BindTotpRequest request, CancellationToken cancellationToken = default);
    Task<JsonElement?> GenerateTotpSecretAsync(CancellationToken cancellationToken = default);
    Task<JsonElement?> GenerateBackupCodesAsync(CancellationToken cancellationToken = default);
    Task<JsonElement?> GetBackupCodesAsync(CancellationToken cancellationToken = default);
    Task UpdateMfaVerificationNameAsync(string verificationId, UpdateMfaVerificationNameRequest request, CancellationToken cancellationToken = default);
    Task DeleteMfaVerificationAsync(string verificationId, CancellationToken cancellationToken = default);

    Task<MyAccountSessionsResult> ListSessionsAsync(CancellationToken cancellationToken = default);
    Task DeleteSessionAsync(string sessionId, RevokeGrantsTarget? revokeGrantsTarget = null, CancellationToken cancellationToken = default);

    Task<MyAccountGrantsResult> ListGrantsAsync(GrantAppType? appType = null, CancellationToken cancellationToken = default);
    Task DeleteGrantAsync(string grantId, CancellationToken cancellationToken = default);

    Task<AvatarUploadResult> UploadAvatarAsync(Stream fileStream, string fileName, string contentType, CancellationToken cancellationToken = default);
}
