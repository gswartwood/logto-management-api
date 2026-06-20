namespace Logto.ManagementApi.EmailTemplates;

public interface IEmailTemplatesClient
{
    Task<IReadOnlyList<EmailTemplate>> ListAsync(string? languageTag = null, EmailTemplateType? templateType = null, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<EmailTemplate>> ReplaceAsync(ReplaceEmailTemplatesRequest request, CancellationToken cancellationToken = default);
    Task<DeleteEmailTemplatesResult> DeleteManyAsync(string? languageTag = null, EmailTemplateType? templateType = null, CancellationToken cancellationToken = default);
    Task<EmailTemplate> GetAsync(string id, CancellationToken cancellationToken = default);
    Task DeleteAsync(string id, CancellationToken cancellationToken = default);
    Task<EmailTemplate> UpdateDetailsAsync(string id, UpdateEmailTemplateDetailsRequest request, CancellationToken cancellationToken = default);
}
