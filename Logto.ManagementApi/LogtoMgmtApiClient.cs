#region Using statements
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
#endregion

namespace Logto.ManagementApi;

public sealed class LogtoMgmtApiClient(HttpClient httpClient) : ILogtoMgmtApiClient
{
    public IAccountCenterClient AccountCenter { get; } = new AccountCenterClient(httpClient);
    public IApplicationsClient Applications { get; } = new ApplicationsClient(httpClient);
    public IAuditLogsClient AuditLogs { get; } = new AuditLogsClient(httpClient);
    public IAuthnClient Authn { get; } = new AuthnClient(httpClient);
    public ICaptchaProviderClient CaptchaProvider { get; } = new CaptchaProviderClient(httpClient);
    public IConfigsClient Configs { get; } = new ConfigsClient(httpClient);
    public IConnectorFactoriesClient ConnectorFactories { get; } = new ConnectorFactoriesClient(httpClient);
    public IConnectorsClient Connectors { get; } = new ConnectorsClient(httpClient);
    public ICustomPhrasesClient CustomPhrases { get; } = new CustomPhrasesClient(httpClient);
    public ICustomProfileFieldsClient CustomProfileFields { get; } = new CustomProfileFieldsClient(httpClient);
    public IDashboardClient Dashboard { get; } = new DashboardClient(httpClient);
    public IDomainsClient Domains { get; } = new DomainsClient(httpClient);
    public IEmailTemplatesClient EmailTemplates { get; } = new EmailTemplatesClient(httpClient);
    public IHooksClient Hooks { get; } = new HooksClient(httpClient);
    public IMyAccountClient MyAccount { get; } = new MyAccountClient(httpClient);
    public IOneTimeTokensClient OneTimeTokens { get; } = new OneTimeTokensClient(httpClient);
    public IOrganizationInvitationsClient OrganizationInvitations { get; } = new OrganizationInvitationsClient(httpClient);
    public IOrganizationsClient Organizations { get; } = new OrganizationsClient(httpClient);
    public IOrganizationRolesClient OrganizationRoles { get; } = new OrganizationRolesClient(httpClient);
    public IOrganizationScopesClient OrganizationScopes { get; } = new OrganizationScopesClient(httpClient);
    public IResourcesClient Resources { get; } = new ResourcesClient(httpClient);
    public IRolesClient Roles { get; } = new RolesClient(httpClient);
    public ISamlApplicationsClient SamlApplications { get; } = new SamlApplicationsClient(httpClient);
    public ISamlClient Saml { get; } = new SamlClient(httpClient);
    public ISecretsClient Secrets { get; } = new SecretsClient(httpClient);
    public ISentinelActivitiesClient SentinelActivities { get; } = new SentinelActivitiesClient(httpClient);
    public ISignInExperienceClient SignInExperience { get; } = new SignInExperienceClient(httpClient);
    public ISsoConnectorProvidersClient SsoConnectorProviders { get; } = new SsoConnectorProvidersClient(httpClient);
    public ISsoConnectorsClient SsoConnectors { get; } = new SsoConnectorsClient(httpClient);
    public IStatusClient Status { get; } = new StatusClient(httpClient);
    public ISubjectTokensClient SubjectTokens { get; } = new SubjectTokensClient(httpClient);
    public ISystemsClient Systems { get; } = new SystemsClient(httpClient);
    public IUserAssetsClient UserAssets { get; } = new UserAssetsClient(httpClient);
    public IUsersClient Users { get; } = new UsersClient(httpClient);
    public IVerificationCodesClient VerificationCodes { get; } = new VerificationCodesClient(httpClient);
    public IVerificationsClient Verifications { get; } = new VerificationsClient(httpClient);
}
