using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Logto.ManagementApi.Utils;

namespace Logto.ManagementApi.OneTimeTokens;

public sealed class OneTimeTokensClient(HttpClient httpClient) : IOneTimeTokensClient
{
    private static readonly JsonSerializerOptions WriteOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
    };

    public async Task<IReadOnlyList<OneTimeToken>> ListAsync(string? email = null, OneTimeTokenStatus? status = null, int? page = null, int? pageSize = null, CancellationToken cancellationToken = default)
    {
        var query = new StringBuilder();
        if (email is not null) query.Append($"email={Uri.EscapeDataString(email)}&");
        if (status is not null) query.Append($"status={Uri.EscapeDataString(JsonSerializer.Serialize(status.Value, WriteOptions).Trim('"'))}&");
        if (page is not null) query.Append($"page={page}&");
        if (pageSize is not null) query.Append($"page_size={pageSize}&");
        var url = query.Length > 0 ? $"one-time-tokens?{query.ToString().TrimEnd('&')}" : "one-time-tokens";
        var response = await httpClient.GetAsync(url, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<OneTimeToken[]>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for one-time-tokens list.");
    }

    public async Task<OneTimeToken> CreateAsync(CreateOneTimeTokenRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync("one-time-tokens", request, WriteOptions, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<OneTimeToken>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for one-time-tokens create.");
    }

    public async Task<OneTimeToken> GetAsync(string id, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync($"one-time-tokens/{Uri.EscapeDataString(id)}", cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<OneTimeToken>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for one-time-tokens get.");
    }

    public async Task DeleteAsync(string id, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.DeleteAsync($"one-time-tokens/{Uri.EscapeDataString(id)}", cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
    }

    public async Task<OneTimeToken> VerifyAsync(VerifyOneTimeTokenRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync("one-time-tokens/verify", request, WriteOptions, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<OneTimeToken>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for one-time-tokens verify.");
    }

    public async Task<OneTimeToken> ReplaceStatusAsync(string id, ReplaceOneTimeTokenStatusRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PutAsJsonAsync($"one-time-tokens/{Uri.EscapeDataString(id)}/status", request, WriteOptions, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<OneTimeToken>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for one-time-tokens replace status.");
    }
}
