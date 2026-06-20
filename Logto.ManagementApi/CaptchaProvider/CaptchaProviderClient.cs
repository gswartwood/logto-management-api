using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using Logto.ManagementApi.Utils;

namespace Logto.ManagementApi.CaptchaProvider;

public sealed class CaptchaProviderClient(HttpClient httpClient) : ICaptchaProviderClient
{
    private static readonly JsonSerializerOptions WriteOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
    };

    public async Task<CaptchaProvider> GetAsync(CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync("captcha-provider", cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<CaptchaProvider>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for captcha provider get.");
    }

    public async Task<CaptchaProvider> UpsertAsync(UpsertCaptchaProviderRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PutAsJsonAsync("captcha-provider", request, WriteOptions, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<CaptchaProvider>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for captcha provider upsert.");
    }

    public async Task DeleteAsync(CancellationToken cancellationToken = default)
    {
        var response = await httpClient.DeleteAsync("captcha-provider", cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
    }
}
