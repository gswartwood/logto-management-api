using System.Text.Json.Serialization;

namespace Logto.ManagementApi.Auth;

internal record LogtoTokenResponse(
    [property: JsonPropertyName("access_token")] string AccessToken,
    [property: JsonPropertyName("expires_in")] int ExpiresIn,
    [property: JsonPropertyName("token_type")] string TokenType
);
