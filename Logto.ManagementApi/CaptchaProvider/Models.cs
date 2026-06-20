using System.Text.Json.Serialization;

namespace Logto.ManagementApi.CaptchaProvider;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum CaptchaProviderType { Turnstile, RecaptchaEnterprise }

[JsonConverter(typeof(JsonStringEnumConverter<RecaptchaMode>))]
public enum RecaptchaMode
{
    [JsonStringEnumMemberName("invisible")]
    Invisible,
    [JsonStringEnumMemberName("checkbox")]
    Checkbox,
}

public record CaptchaProviderConfig
{
    public required CaptchaProviderType Type { get; init; }
    public required string SiteKey { get; init; }
    public required string SecretKey { get; init; }
    public string? ProjectId { get; init; }
    public string? Domain { get; init; }
    public RecaptchaMode? Mode { get; init; }
}

public record CaptchaProvider
{
    public required string TenantId { get; init; }
    public required string Id { get; init; }
    public required CaptchaProviderConfig Config { get; init; }
    public required double CreatedAt { get; init; }
    public required double UpdatedAt { get; init; }
}

public record UpsertCaptchaProviderRequest
{
    public required CaptchaProviderConfig Config { get; init; }
}
