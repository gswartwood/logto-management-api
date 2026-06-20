using System.Text.Json.Serialization;

namespace Logto.ManagementApi.OneTimeTokens;

[JsonConverter(typeof(JsonStringEnumConverter<OneTimeTokenStatus>))]
public enum OneTimeTokenStatus
{
    [JsonStringEnumMemberName("active")] Active,
    [JsonStringEnumMemberName("consumed")] Consumed,
    [JsonStringEnumMemberName("revoked")] Revoked,
    [JsonStringEnumMemberName("expired")] Expired,
}

public record OneTimeTokenContext(IReadOnlyList<string>? JitOrganizationIds);

public record OneTimeToken(
    string TenantId,
    string Id,
    string Email,
    string Token,
    OneTimeTokenContext Context,
    OneTimeTokenStatus Status,
    double CreatedAt,
    double ExpiresAt);

public record CreateOneTimeTokenRequest(
    string Email,
    OneTimeTokenContext? Context,
    double? ExpiresIn);

public record VerifyOneTimeTokenRequest(string Token, string Email);

public record ReplaceOneTimeTokenStatusRequest(OneTimeTokenStatus Status);
