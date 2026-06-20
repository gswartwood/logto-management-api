namespace Logto.ManagementApi.Sso;

public interface ISsoConnectorProvidersClient
{
    Task<IReadOnlyList<SsoConnectorProvider>> ListAsync(CancellationToken cancellationToken = default);
}
