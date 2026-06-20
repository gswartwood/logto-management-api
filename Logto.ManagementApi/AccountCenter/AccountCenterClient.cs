using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using Logto.ManagementApi.Utils;

namespace Logto.ManagementApi.AccountCenter;

public sealed class AccountCenterClient(HttpClient httpClient) : IAccountCenterClient
{
    private static readonly JsonSerializerOptions PatchOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
    };

    public async Task<AccountCenterSettings> GetAsync(CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync("account-center", cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<AccountCenterSettings>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for account center get.");
    }

    public async Task<AccountCenterSettings> UpdateAsync(UpdateAccountCenterSettings request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PatchAsJsonAsync("account-center", request, PatchOptions, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<AccountCenterSettings>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for account center update.");
    }
}
