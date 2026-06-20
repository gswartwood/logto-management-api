namespace Logto.ManagementApi.SentinelActivities;

public interface ISentinelActivitiesClient
{
    Task DeleteAsync(DeleteSentinelActivitiesRequest request, CancellationToken cancellationToken = default);
}
