using Logto.ManagementApi.Utils;

namespace Logto.ManagementApi.Status;

public sealed class StatusClient(HttpClient httpClient) : IStatusClient
{
    public async Task CheckAsync(CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync("status", cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
    }
}
