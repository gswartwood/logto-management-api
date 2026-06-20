using Logto.ManagementApi.AccountCenter;
using Logto.ManagementApi.Applications;
using Logto.ManagementApi.AuditLogs;
using Logto.ManagementApi.Authn;
using Logto.ManagementApi.CaptchaProvider;
using Logto.ManagementApi.Configs;
using Logto.ManagementApi.Connectors;
using Logto.ManagementApi.CustomPhrases;
using Logto.ManagementApi.CustomProfileFields;
using Logto.ManagementApi.Dashboard;
using Logto.ManagementApi.Domains;
using Logto.ManagementApi.EmailTemplates;
using Logto.ManagementApi.Hooks;
using Logto.ManagementApi.MyAccount;
using Logto.ManagementApi.OneTimeTokens;
using Logto.ManagementApi.Organizations;
using Logto.ManagementApi.Resources;
using Logto.ManagementApi.Roles;
using Logto.ManagementApi.Saml;
using Logto.ManagementApi.Secrets;
using Logto.ManagementApi.SentinelActivities;
using Logto.ManagementApi.SignInExperience;
using Logto.ManagementApi.Sso;
using Logto.ManagementApi.Status;
using Logto.ManagementApi.SubjectTokens;
using Logto.ManagementApi.Systems;
using Logto.ManagementApi.Users;
using Logto.ManagementApi.Verifications;

namespace Logto.ManagementApi;

public interface ILogtoMgmtApiClient
{
    IAccountCenterClient AccountCenter { get; }
    IApplicationsClient Applications { get; }
    IAuditLogsClient AuditLogs { get; }
    IAuthnClient Authn { get; }
    ICaptchaProviderClient CaptchaProvider { get; }
    IConfigsClient Configs { get; }
    IConnectorFactoriesClient ConnectorFactories { get; }
    IConnectorsClient Connectors { get; }
    ICustomPhrasesClient CustomPhrases { get; }
    ICustomProfileFieldsClient CustomProfileFields { get; }
    IDashboardClient Dashboard { get; }
    IDomainsClient Domains { get; }
    IEmailTemplatesClient EmailTemplates { get; }
    IHooksClient Hooks { get; }
    IMyAccountClient MyAccount { get; }
    IOneTimeTokensClient OneTimeTokens { get; }
    IOrganizationInvitationsClient OrganizationInvitations { get; }
    IOrganizationsClient Organizations { get; }
    IOrganizationRolesClient OrganizationRoles { get; }
    IOrganizationScopesClient OrganizationScopes { get; }
    IResourcesClient Resources { get; }
    IRolesClient Roles { get; }
    ISamlApplicationsClient SamlApplications { get; }
    ISamlClient Saml { get; }
    ISecretsClient Secrets { get; }
    ISentinelActivitiesClient SentinelActivities { get; }
    ISignInExperienceClient SignInExperience { get; }
    ISsoConnectorProvidersClient SsoConnectorProviders { get; }
    ISsoConnectorsClient SsoConnectors { get; }
    IStatusClient Status { get; }
    ISubjectTokensClient SubjectTokens { get; }
    ISystemsClient Systems { get; }
    IUserAssetsClient UserAssets { get; }
    IUsersClient Users { get; }
    IVerificationCodesClient VerificationCodes { get; }
    IVerificationsClient Verifications { get; }
}
