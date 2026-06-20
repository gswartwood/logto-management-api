using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Logto.ManagementApi.Utils;

namespace Logto.ManagementApi.Hooks;

public sealed class HooksClient(HttpClient httpClient) : IHooksClient
{
    private static readonly JsonSerializerOptions WriteOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
    };

    public async Task<IReadOnlyList<Hook>> ListAsync(bool? includeExecutionStats = null, int? page = null, int? pageSize = null, CancellationToken cancellationToken = default)
    {
        var url = BuildUrl("hooks", includeExecutionStats, page, pageSize);
        var response = await httpClient.GetAsync(url, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<Hook[]>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for hooks list.");
    }

    public async Task<Hook> CreateAsync(CreateHookRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync("hooks", request, WriteOptions, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<Hook>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for hooks create.");
    }

    public async Task<Hook> GetAsync(string id, bool? includeExecutionStats = null, CancellationToken cancellationToken = default)
    {
        var url = BuildUrl($"hooks/{Uri.EscapeDataString(id)}", includeExecutionStats, null, null);
        var response = await httpClient.GetAsync(url, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<Hook>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for hooks get.");
    }

    public async Task<Hook> UpdateAsync(string id, UpdateHookRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PatchAsJsonAsync($"hooks/{Uri.EscapeDataString(id)}", request, WriteOptions, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<Hook>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for hooks update.");
    }

    public async Task DeleteAsync(string id, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.DeleteAsync($"hooks/{Uri.EscapeDataString(id)}", cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<HookRecentLog>> ListRecentLogsAsync(string id, ListHookRecentLogsRequest? request = null, CancellationToken cancellationToken = default)
    {
        var query = new StringBuilder();
        if (request?.LogKey is not null) query.Append($"logKey={Uri.EscapeDataString(request.LogKey)}&");
        if (request?.EnableCap is not null) query.Append($"enableCap={request.EnableCap.Value.ToString().ToLowerInvariant()}&");
        if (request?.StartTime is not null) query.Append($"start_time={Uri.EscapeDataString(request.StartTime)}&");
        if (request?.EndTime is not null) query.Append($"end_time={Uri.EscapeDataString(request.EndTime)}&");
        if (request?.Page is not null) query.Append($"page={request.Page}&");
        if (request?.PageSize is not null) query.Append($"page_size={request.PageSize}&");
        var url = query.Length > 0
            ? $"hooks/{Uri.EscapeDataString(id)}/recent-logs?{query.ToString().TrimEnd('&')}"
            : $"hooks/{Uri.EscapeDataString(id)}/recent-logs";
        var response = await httpClient.GetAsync(url, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<HookRecentLog[]>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for hooks recent-logs.");
    }

    public async Task TestAsync(string id, TestHookRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync($"hooks/{Uri.EscapeDataString(id)}/test", request, WriteOptions, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
    }

    public async Task<Hook> UpdateSigningKeyAsync(string id, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PatchAsync($"hooks/{Uri.EscapeDataString(id)}/signing-key", null, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<Hook>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for hooks update signing key.");
    }

    private static string BuildUrl(string base_, bool? includeExecutionStats, int? page, int? pageSize)
    {
        var query = new StringBuilder();
        if (includeExecutionStats is true) query.Append("includeExecutionStats=true&");
        if (page is not null) query.Append($"page={page}&");
        if (pageSize is not null) query.Append($"page_size={pageSize}&");
        return query.Length > 0 ? $"{base_}?{query.ToString().TrimEnd('&')}" : base_;
    }
}
