using System.Text.Json;
using System.Text.Json.Serialization;

namespace Logto.ManagementApi.AuditLogs;

public record AuditLog
{
    public required string TenantId { get; init; }
    public required string Id { get; init; }
    public required string Key { get; init; }
    public required AuditLogPayload Payload { get; init; }
    public required double CreatedAt { get; init; }
}

public record AuditLogPayload
{
    public required string Key { get; init; }
    public required AuditLogResult Result { get; init; }
    public JsonElement? Error { get; init; }
    public string? Ip { get; init; }
    public string? UserAgent { get; init; }
    public UserAgentParsed? UserAgentParsed { get; init; }
    public string? UserId { get; init; }
    public string? ApplicationId { get; init; }
    public string? SessionId { get; init; }
    public Dictionary<string, object>? Params { get; init; }
}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum AuditLogResult
{
    Success,
    Error,
}

public record UserAgentParsed
{
    public string? Ua { get; init; }
    public UserAgentBrowser? Browser { get; init; }
    public UserAgentDevice? Device { get; init; }
    public UserAgentEngine? Engine { get; init; }
    public UserAgentOs? Os { get; init; }
    public UserAgentCpu? Cpu { get; init; }
}

public record UserAgentBrowser
{
    public string? Name { get; init; }
    public string? Version { get; init; }
    public string? Major { get; init; }
    public string? Type { get; init; }
}

public record UserAgentDevice
{
    public string? Model { get; init; }
    public string? Type { get; init; }
    public string? Vendor { get; init; }
}

public record UserAgentEngine
{
    public string? Name { get; init; }
    public string? Version { get; init; }
}

public record UserAgentOs
{
    public string? Name { get; init; }
    public string? Version { get; init; }
}

public record UserAgentCpu
{
    public string? Architecture { get; init; }
}

public record AuditLogsListOptions
{
    public string? UserId { get; init; }
    public string? ApplicationId { get; init; }
    public string? LogKey { get; init; }
    public bool? EnableCap { get; init; }
    public long? StartTime { get; init; }
    public long? EndTime { get; init; }
    public int? Page { get; init; }
    public int? PageSize { get; init; }
}
