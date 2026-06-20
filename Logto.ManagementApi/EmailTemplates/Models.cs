using System.Text.Json.Serialization;

namespace Logto.ManagementApi.EmailTemplates;

[JsonConverter(typeof(JsonStringEnumConverter<EmailTemplateType>))]
public enum EmailTemplateType
{
    SignIn,
    Register,
    ForgotPassword,
    OrganizationInvitation,
    Generic,
    UserPermissionValidation,
    BindNewIdentifier,
    MfaVerification,
    BindMfa,
}

public record EmailTemplateDetails(
    string Subject,
    string Content,
    string? ContentType,
    string? ReplyTo,
    string? SendFrom);

public record EmailTemplate(
    string TenantId,
    string Id,
    string LanguageTag,
    EmailTemplateType TemplateType,
    EmailTemplateDetails Details,
    double CreatedAt);

public record EmailTemplateInput(
    string LanguageTag,
    EmailTemplateType TemplateType,
    EmailTemplateDetails Details);

public record ReplaceEmailTemplatesRequest(IReadOnlyList<EmailTemplateInput> Templates);

public record UpdateEmailTemplateDetailsRequest(
    string? Subject,
    string? Content,
    string? ContentType,
    string? ReplyTo,
    string? SendFrom);

public record DeleteEmailTemplatesResult(double RowCount);
