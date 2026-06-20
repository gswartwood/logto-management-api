using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Logto.ManagementApi.Utils;

namespace Logto.ManagementApi.Authn;

public sealed class AuthnClient(HttpClient httpClient) : IAuthnClient
{
    private static readonly JsonSerializerOptions PostOptions = new()
    {
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
    };

    public async Task<HasuraAuthClaims> GetHasuraAuthAsync(string resource, string? unauthorizedRole = null, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync(BuildHasuraUrl(resource, unauthorizedRole), cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<HasuraAuthClaims>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for Hasura auth.");
    }

    public async Task AssertSingleSignOnSamlAsync(string connectorId, AssertSamlRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync($"authn/single-sign-on/saml/{Uri.EscapeDataString(connectorId)}", request, PostOptions, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
    }

    private static string BuildHasuraUrl(string resource, string? unauthorizedRole)
    {
        var query = new StringBuilder();
        query.Append("?resource=");
        query.Append(Uri.EscapeDataString(resource));

        if (unauthorizedRole is not null)
        {
            query.Append("&unauthorizedRole=");
            query.Append(Uri.EscapeDataString(unauthorizedRole));
        }

        return $"authn/hasura{query}";
    }
}
