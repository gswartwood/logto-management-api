using System.Net.Http.Json;
using Logto.ManagementApi.Utils;

namespace Logto.ManagementApi.Systems;

public sealed class SystemsClient(HttpClient httpClient) : ISystemsClient
{
    public async Task<SystemApplicationConfig> GetApplicationConfigAsync(CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync("systems/application", cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<SystemApplicationConfig>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for systems/application get.");
    }
}
