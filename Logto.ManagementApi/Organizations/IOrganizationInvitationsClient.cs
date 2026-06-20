namespace Logto.ManagementApi.Organizations;

public interface IOrganizationInvitationsClient
{
    Task<IReadOnlyList<OrganizationInvitation>> ListAsync(string? organizationId = null, string? inviterId = null, string? invitee = null, CancellationToken cancellationToken = default);
    Task<OrganizationInvitation> CreateAsync(CreateOrganizationInvitationRequest request, CancellationToken cancellationToken = default);
    Task<OrganizationInvitation> GetAsync(string id, CancellationToken cancellationToken = default);
    Task DeleteAsync(string id, CancellationToken cancellationToken = default);
    Task ResendMessageAsync(string id, OrganizationInvitationMessagePayload request, CancellationToken cancellationToken = default);
    Task<OrganizationInvitation> ReplaceStatusAsync(string id, ReplaceOrganizationInvitationStatusRequest request, CancellationToken cancellationToken = default);
}
