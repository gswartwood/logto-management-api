namespace Logto.ManagementApi.Verifications;

public interface IVerificationCodesClient
{
    Task SendAsync(CreateVerificationCodeRequest request, CancellationToken cancellationToken = default);
    Task VerifyAsync(VerifyVerificationCodeRequest request, CancellationToken cancellationToken = default);
}
