using Logto.ManagementApi.Utils;

namespace Logto.ManagementApi.Secrets;

public sealed class SecretsClient(HttpClient httpClient) : ISecretsClient
{
    public async Task DeleteAsync(string id, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.DeleteAsync($"secrets/{Uri.EscapeDataString(id)}", cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
    }
}
