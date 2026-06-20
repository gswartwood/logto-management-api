using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Logto.ManagementApi.Utils;

namespace Logto.ManagementApi.Organizations;

public sealed class OrganizationInvitationsClient(HttpClient httpClient) : IOrganizationInvitationsClient
{
    private static readonly JsonSerializerOptions WriteOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
    };

    public async Task<IReadOnlyList<OrganizationInvitation>> ListAsync(string? organizationId = null, string? inviterId = null, string? invitee = null, CancellationToken cancellationToken = default)
    {
        var query = new StringBuilder();
        if (organizationId is not null) query.Append($"organizationId={Uri.EscapeDataString(organizationId)}&");
        if (inviterId is not null) query.Append($"inviterId={Uri.EscapeDataString(inviterId)}&");
        if (invitee is not null) query.Append($"invitee={Uri.EscapeDataString(invitee)}&");
        var url = query.Length > 0 ? $"organization-invitations?{query.ToString().TrimEnd('&')}" : "organization-invitations";
        var response = await httpClient.GetAsync(url, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<OrganizationInvitation[]>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for organization-invitations list.");
    }

    public async Task<OrganizationInvitation> CreateAsync(CreateOrganizationInvitationRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync("organization-invitations", request, WriteOptions, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<OrganizationInvitation>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for organization-invitations create.");
    }

    public async Task<OrganizationInvitation> GetAsync(string id, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync($"organization-invitations/{Uri.EscapeDataString(id)}", cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<OrganizationInvitation>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for organization-invitations get.");
    }

    public async Task DeleteAsync(string id, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.DeleteAsync($"organization-invitations/{Uri.EscapeDataString(id)}", cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
    }

    public async Task ResendMessageAsync(string id, OrganizationInvitationMessagePayload request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync($"organization-invitations/{Uri.EscapeDataString(id)}/message", request, WriteOptions, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
    }

    public async Task<OrganizationInvitation> ReplaceStatusAsync(string id, ReplaceOrganizationInvitationStatusRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PutAsJsonAsync($"organization-invitations/{Uri.EscapeDataString(id)}/status", request, WriteOptions, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<OrganizationInvitation>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for organization-invitations replace status.");
    }
}
