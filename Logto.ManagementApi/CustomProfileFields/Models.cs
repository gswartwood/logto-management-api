using System.Text.Json.Serialization;

namespace Logto.ManagementApi.CustomProfileFields;

[JsonConverter(typeof(JsonStringEnumConverter<CustomProfileFieldType>))]
public enum CustomProfileFieldType
{
    Text,
    Number,
    Date,
    Checkbox,
    Select,
    Url,
    Regex,
    Address,
    Fullname,
}

public record CustomProfileFieldOption(
    string? Label,
    string Value
);

public record CustomProfileFieldConfig(
    string? Placeholder,
    double? MinLength,
    double? MaxLength,
    double? MinValue,
    double? MaxValue,
    string? Format,
    string? CustomFormat,
    IReadOnlyList<CustomProfileFieldOption>? Options,
    string? DefaultValue,
    IReadOnlyList<CustomProfileFieldPart>? Parts
);

public record CustomProfileFieldPart(
    bool Enabled,
    string Name,
    CustomProfileFieldType Type,
    string Label,
    string? Description,
    bool Required,
    CustomProfileFieldConfig? Config
);

public record CustomProfileField(
    string TenantId,
    string Id,
    string Name,
    CustomProfileFieldType Type,
    string Label,
    string? Description,
    bool Required,
    CustomProfileFieldConfig Config,
    double CreatedAt,
    double SieOrder
);

public record CreateCustomProfileFieldRequest(
    string Name,
    CustomProfileFieldType Type,
    bool Required,
    string? Label,
    string? Description,
    CustomProfileFieldConfig? Config
);

public record UpdateCustomProfileFieldRequest(
    CustomProfileFieldType Type,
    bool Required,
    string? Label,
    string? Description,
    CustomProfileFieldConfig? Config
);

public record CustomProfileFieldSieOrderItem(
    string Name,
    double SieOrder
);

public record UpdateCustomProfileFieldsSieOrderRequest(
    IReadOnlyList<CustomProfileFieldSieOrderItem> Order
);
