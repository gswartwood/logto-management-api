namespace Logto.ManagementApi.Status;

public interface IStatusClient
{
    Task CheckAsync(CancellationToken cancellationToken = default);
}
