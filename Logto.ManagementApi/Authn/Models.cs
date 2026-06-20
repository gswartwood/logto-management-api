using System.Text.Json.Serialization;

namespace Logto.ManagementApi.Authn;

public record HasuraAuthClaims
{
    [JsonPropertyName("X-Hasura-User-Id")]
    public string? XHasuraUserId { get; init; }

    [JsonPropertyName("X-Hasura-Role")]
    public string? XHasuraRole { get; init; }
}

public record AssertSamlRequest
{
    [JsonPropertyName("RelayState")]
    public string? RelayState { get; init; }

    [JsonPropertyName("SAMLResponse")]
    public required string SamlResponse { get; init; }
}
