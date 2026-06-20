using System.Text.Json.Serialization;

namespace Logto.ManagementApi.AccountCenter;

public record AccountCenterSettings
{
    public required string TenantId { get; init; }
    public required string Id { get; init; }
    public required bool Enabled { get; init; }
    public required AccountCenterFields Fields { get; init; }
    public required List<string> WebauthnRelatedOrigins { get; init; }
    public required string? DeleteAccountUrl { get; init; }
    public required string? CustomCss { get; init; }
    public required List<ProfileField>? ProfileFields { get; init; }
}

public record AccountCenterFields
{
    public AccountCenterFieldMode? Name { get; init; }
    public AccountCenterFieldMode? Avatar { get; init; }
    public AccountCenterFieldMode? Profile { get; init; }
    public AccountCenterFieldMode? Email { get; init; }
    public AccountCenterFieldMode? Phone { get; init; }
    public AccountCenterFieldMode? Password { get; init; }
    public AccountCenterFieldMode? Username { get; init; }
    public AccountCenterFieldMode? Social { get; init; }
    public AccountCenterFieldMode? CustomData { get; init; }
    public AccountCenterFieldMode? Mfa { get; init; }
    public AccountCenterFieldMode? Passkey { get; init; }
    public AccountCenterFieldMode? Session { get; init; }
}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum AccountCenterFieldMode
{
    Off,
    ReadOnly,
    Edit,
}

public record ProfileField
{
    public required string Name { get; init; }
}

public record UpdateAccountCenterSettings
{
    public bool? Enabled { get; init; }
    public AccountCenterFields? Fields { get; init; }
    public List<string>? WebauthnRelatedOrigins { get; init; }
    public string? DeleteAccountUrl { get; init; }
    public string? CustomCss { get; init; }
    public List<ProfileField>? ProfileFields { get; init; }
}
