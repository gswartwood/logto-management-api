namespace Logto.ManagementApi.Dashboard;

public interface IDashboardClient
{
    Task<TotalUserCountResult> GetTotalUserCountAsync(CancellationToken cancellationToken = default);
    Task<NewUserCounts> GetNewUserCountsAsync(CancellationToken cancellationToken = default);
    Task<ActiveUserCounts> GetActiveUserCountsAsync(string? date = null, CancellationToken cancellationToken = default);
}
