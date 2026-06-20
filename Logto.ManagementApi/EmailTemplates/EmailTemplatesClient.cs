using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Logto.ManagementApi.Utils;

namespace Logto.ManagementApi.EmailTemplates;

public sealed class EmailTemplatesClient(HttpClient httpClient) : IEmailTemplatesClient
{
    private static readonly JsonSerializerOptions WriteOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
    };

    public async Task<IReadOnlyList<EmailTemplate>> ListAsync(string? languageTag = null, EmailTemplateType? templateType = null, CancellationToken cancellationToken = default)
    {
        var url = BuildFilterUrl("email-templates", languageTag, templateType);
        var response = await httpClient.GetAsync(url, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<EmailTemplate[]>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for email-templates list.");
    }

    public async Task<IReadOnlyList<EmailTemplate>> ReplaceAsync(ReplaceEmailTemplatesRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PutAsJsonAsync("email-templates", request, WriteOptions, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<EmailTemplate[]>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for email-templates replace.");
    }

    public async Task<DeleteEmailTemplatesResult> DeleteManyAsync(string? languageTag = null, EmailTemplateType? templateType = null, CancellationToken cancellationToken = default)
    {
        var url = BuildFilterUrl("email-templates", languageTag, templateType);
        var response = await httpClient.DeleteAsync(url, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<DeleteEmailTemplatesResult>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for email-templates delete.");
    }

    public async Task<EmailTemplate> GetAsync(string id, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync($"email-templates/{Uri.EscapeDataString(id)}", cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<EmailTemplate>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for email-templates get.");
    }

    public async Task DeleteAsync(string id, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.DeleteAsync($"email-templates/{Uri.EscapeDataString(id)}", cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
    }

    public async Task<EmailTemplate> UpdateDetailsAsync(string id, UpdateEmailTemplateDetailsRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PatchAsJsonAsync($"email-templates/{Uri.EscapeDataString(id)}/details", request, WriteOptions, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<EmailTemplate>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for email-templates update details.");
    }

    private static string BuildFilterUrl(string baseUrl, string? languageTag, EmailTemplateType? templateType)
    {
        var query = new StringBuilder();
        if (languageTag is not null)
            query.Append($"languageTag={Uri.EscapeDataString(languageTag)}&");
        if (templateType is not null)
            query.Append($"templateType={Uri.EscapeDataString(templateType.Value.ToString())}&");
        return query.Length > 0 ? $"{baseUrl}?{query.ToString().TrimEnd('&')}" : baseUrl;
    }
}
