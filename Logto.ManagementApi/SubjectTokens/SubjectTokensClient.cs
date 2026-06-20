using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using Logto.ManagementApi.Utils;

namespace Logto.ManagementApi.SubjectTokens;

public sealed class SubjectTokensClient(HttpClient httpClient) : ISubjectTokensClient
{
    private static readonly JsonSerializerOptions WriteOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
    };

    public async Task<SubjectToken> CreateAsync(CreateSubjectTokenRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync("subject-tokens", request, WriteOptions, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<SubjectToken>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for subject-tokens create.");
    }
}
