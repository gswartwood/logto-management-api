using System.Net.Http.Json;
using System.Text;
using Logto.ManagementApi.Utils;

namespace Logto.ManagementApi.AuditLogs;

public sealed class AuditLogsClient(HttpClient httpClient) : IAuditLogsClient
{
    public async Task<IEnumerable<AuditLog>> ListAsync(AuditLogsListOptions? options = null, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync(BuildListUrl(options), cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<IEnumerable<AuditLog>>(cancellationToken: cancellationToken) ?? [];
    }

    public async Task<AuditLog> GetAsync(string id, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync($"logs/{Uri.EscapeDataString(id)}", cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<AuditLog>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for log get.");
    }

    private static string BuildListUrl(AuditLogsListOptions? options)
    {
        if (options is null)
            return "logs";

        var query = new StringBuilder();

        void Append(string key, string value)
        {
            query.Append(query.Length == 0 ? '?' : '&');
            query.Append(Uri.EscapeDataString(key));
            query.Append('=');
            query.Append(Uri.EscapeDataString(value));
        }

        if (options.UserId is not null)
            Append("userId", options.UserId);

        if (options.ApplicationId is not null)
            Append("applicationId", options.ApplicationId);

        if (options.LogKey is not null)
            Append("logKey", options.LogKey);

        if (options.EnableCap is not null)
            Append("enableCap", options.EnableCap.Value ? "true" : "false");

        if (options.StartTime is not null)
            Append("start_time", options.StartTime.Value.ToString());

        if (options.EndTime is not null)
            Append("end_time", options.EndTime.Value.ToString());

        if (options.Page is not null)
            Append("page", options.Page.Value.ToString());

        if (options.PageSize is not null)
            Append("page_size", options.PageSize.Value.ToString());

        return $"logs{query}";
    }
}
