using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Logto.ManagementApi.Utils;

namespace Logto.ManagementApi.Saml;

public sealed class SamlClient(HttpClient httpClient) : ISamlClient
{
    // SAMLRequest and RelayState must not be camelCased
    private static readonly JsonSerializerOptions WriteOptions = new()
    {
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
    };

    public async Task GetAuthnAsync(string id, string samlRequest, string? signature = null, string? sigAlg = null, string? relayState = null, CancellationToken cancellationToken = default)
    {
        var query = new StringBuilder($"SAMLRequest={Uri.EscapeDataString(samlRequest)}");
        if (signature is not null) query.Append($"&Signature={Uri.EscapeDataString(signature)}");
        if (sigAlg is not null) query.Append($"&SigAlg={Uri.EscapeDataString(sigAlg)}");
        if (relayState is not null) query.Append($"&RelayState={Uri.EscapeDataString(relayState)}");
        var response = await httpClient.GetAsync($"saml/{Uri.EscapeDataString(id)}/authn?{query}", cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
    }

    public async Task CreateAuthnAsync(string id, CreateSamlAuthnRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync($"saml/{Uri.EscapeDataString(id)}/authn", request, WriteOptions, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
    }
}
