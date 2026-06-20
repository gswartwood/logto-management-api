using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using Logto.ManagementApi.Utils;

namespace Logto.ManagementApi.CustomPhrases;

public sealed class CustomPhrasesClient(HttpClient httpClient) : ICustomPhrasesClient
{
    private static readonly JsonSerializerOptions WriteOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
    };

    public async Task<IReadOnlyList<CustomPhrase>> ListAsync(CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync("custom-phrases", cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<IReadOnlyList<CustomPhrase>>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for custom phrases list.");
    }

    public async Task<CustomPhrase> GetAsync(string languageTag, CancellationToken cancellationToken = default)
    {
        var tag = Uri.EscapeDataString(languageTag);
        var response = await httpClient.GetAsync($"custom-phrases/{tag}", cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<CustomPhrase>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for custom phrase get.");
    }

    public async Task<CustomPhrase> UpsertAsync(string languageTag, Dictionary<string, object> translation, CancellationToken cancellationToken = default)
    {
        var tag = Uri.EscapeDataString(languageTag);
        var response = await httpClient.PutAsJsonAsync($"custom-phrases/{tag}", translation, WriteOptions, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<CustomPhrase>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for custom phrase upsert.");
    }

    public async Task DeleteAsync(string languageTag, CancellationToken cancellationToken = default)
    {
        var tag = Uri.EscapeDataString(languageTag);
        var response = await httpClient.DeleteAsync($"custom-phrases/{tag}", cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
    }
}
