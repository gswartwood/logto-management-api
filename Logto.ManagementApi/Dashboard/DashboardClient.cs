using System.Net.Http.Json;
using Logto.ManagementApi.Utils;

namespace Logto.ManagementApi.Dashboard;

public sealed class DashboardClient(HttpClient httpClient) : IDashboardClient
{
    public async Task<TotalUserCountResult> GetTotalUserCountAsync(CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync("dashboard/users/total", cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<TotalUserCountResult>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for dashboard/users/total.");
    }

    public async Task<NewUserCounts> GetNewUserCountsAsync(CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync("dashboard/users/new", cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<NewUserCounts>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for dashboard/users/new.");
    }

    public async Task<ActiveUserCounts> GetActiveUserCountsAsync(string? date = null, CancellationToken cancellationToken = default)
    {
        var url = date is not null
            ? $"dashboard/users/active?date={Uri.EscapeDataString(date)}"
            : "dashboard/users/active";
        var response = await httpClient.GetAsync(url, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<ActiveUserCounts>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for dashboard/users/active.");
    }
}
