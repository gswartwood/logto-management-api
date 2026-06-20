namespace Logto.ManagementApi.CaptchaProvider;

public interface ICaptchaProviderClient
{
    Task<CaptchaProvider> GetAsync(CancellationToken cancellationToken = default);
    Task<CaptchaProvider> UpsertAsync(UpsertCaptchaProviderRequest request, CancellationToken cancellationToken = default);
    Task DeleteAsync(CancellationToken cancellationToken = default);
}
