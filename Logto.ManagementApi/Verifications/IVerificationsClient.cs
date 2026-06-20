namespace Logto.ManagementApi.Verifications;

public interface IVerificationsClient
{
    Task<VerificationRecord> CreateByPasswordAsync(CreatePasswordVerificationRequest request, CancellationToken cancellationToken = default);
    Task<VerificationRecord> CreateByVerificationCodeAsync(CreateVerificationCodeVerificationRequest request, CancellationToken cancellationToken = default);
    Task<VerificationRecordResult> VerifyVerificationCodeAsync(VerifyVerificationCodeVerificationRequest request, CancellationToken cancellationToken = default);
    Task<SocialVerificationResult> CreateBySocialAsync(CreateSocialVerificationRequest request, CancellationToken cancellationToken = default);
    Task<VerificationRecordResult> VerifySocialAsync(VerifySocialVerificationRequest request, CancellationToken cancellationToken = default);
    Task<WebAuthnRegistrationResult> GenerateWebAuthnRegistrationOptionsAsync(CancellationToken cancellationToken = default);
    Task<VerificationRecordResult> VerifyWebAuthnRegistrationAsync(VerifyWebAuthnRegistrationRequest request, CancellationToken cancellationToken = default);
}
