using System.Net.Http.Json;
using Logto.ManagementApi.Utils;

namespace Logto.ManagementApi.Sso;

public sealed class SsoConnectorProvidersClient(HttpClient httpClient) : ISsoConnectorProvidersClient
{
    public async Task<IReadOnlyList<SsoConnectorProvider>> ListAsync(CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync("sso-connector-providers", cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<SsoConnectorProvider[]>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for sso-connector-providers list.");
    }
}
