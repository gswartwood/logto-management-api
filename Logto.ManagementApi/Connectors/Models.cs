using System.Text.Json;
using System.Text.Json.Serialization;

namespace Logto.ManagementApi.Connectors;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ConnectorType { Email, Sms, Social }

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ConnectorPlatform { Native, Universal, Web }

public record ConnectorFactory
{
    public required ConnectorType Type { get; init; }
    public required string Id { get; init; }
    public required string Target { get; init; }
    public required Dictionary<string, object> Name { get; init; }
    public required Dictionary<string, object> Description { get; init; }
    public required string Logo { get; init; }
    public required string? LogoDark { get; init; }
    public required string Readme { get; init; }
    public required ConnectorPlatform? Platform { get; init; }
    public bool? IsDemo { get; init; }
    public string? ConfigTemplate { get; init; }
    public List<ConnectorFormItem>? FormItems { get; init; }
    public Dictionary<string, object>? CustomData { get; init; }
    public string? FromEmail { get; init; }
    public bool? IsStandard { get; init; }
    public bool? IsTokenStorageSupported { get; init; }
}

public record ConnectorMetadata
{
    public string? Target { get; init; }
    public Dictionary<string, object>? Name { get; init; }
    public string? Logo { get; init; }
    public string? LogoDark { get; init; }
}

public record Connector : ConnectorFactory
{
    public required bool SyncProfile { get; init; }
    public required bool EnableTokenStorage { get; init; }
    public required Dictionary<string, object> Config { get; init; }
    public required ConnectorMetadata Metadata { get; init; }
    public required string ConnectorId { get; init; }
    public Dictionary<string, object>? ExtraInfo { get; init; }
    public double? Usage { get; init; }
}

public record ConnectorFormItem
{
    public required string Type { get; init; }
    public required string Key { get; init; }
    public required string Label { get; init; }
    public List<ConnectorFormSelectItem>? SelectItems { get; init; }
    public string? Placeholder { get; init; }
    public bool? Required { get; init; }
    public JsonElement? DefaultValue { get; init; }
    public List<ConnectorFormShowCondition>? ShowConditions { get; init; }
    public string? Description { get; init; }
    public string? Tooltip { get; init; }
    public bool? IsConfidential { get; init; }
    public bool? IsDevFeature { get; init; }
}

public record ConnectorFormSelectItem
{
    public required string Value { get; init; }
    public string? Title { get; init; }
}

public record ConnectorFormShowCondition
{
    public required string TargetKey { get; init; }
    public JsonElement? ExpectValue { get; init; }
}

public record CreateConnectorRequest
{
    public required string ConnectorId { get; init; }
    public Dictionary<string, object>? Config { get; init; }
    public ConnectorMetadata? Metadata { get; init; }
    public bool? SyncProfile { get; init; }
    public bool? EnableTokenStorage { get; init; }
    public string? Id { get; init; }
}

public record UpdateConnectorRequest
{
    public Dictionary<string, object>? Config { get; init; }
    public ConnectorMetadata? Metadata { get; init; }
    public bool? SyncProfile { get; init; }
    public bool? EnableTokenStorage { get; init; }
}

public record TestConnectorRequest
{
    public required Dictionary<string, object> Config { get; init; }
    public string? Phone { get; init; }
    public string? Email { get; init; }
    public string? Locale { get; init; }
}

public record GetAuthorizationUriRequest
{
    public required string State { get; init; }
    public required string RedirectUri { get; init; }
}

public record ConnectorAuthorizationUri
{
    public required string RedirectTo { get; init; }
}
