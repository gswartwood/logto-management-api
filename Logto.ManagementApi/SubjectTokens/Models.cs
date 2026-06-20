using System.Text.Json.Serialization;

namespace Logto.ManagementApi.SubjectTokens;

public record CreateSubjectTokenRequest(
    string UserId,
    Dictionary<string, object>? Context = null
);

public record SubjectToken(
    [property: JsonPropertyName("subjectToken")] string Token,
    double ExpiresIn
);
