namespace Logto.ManagementApi.Systems;

public interface ISystemsClient
{
    Task<SystemApplicationConfig> GetApplicationConfigAsync(CancellationToken cancellationToken = default);
}
