using System.Text.Json;
using System.Text.Json.Serialization;

namespace Logto.ManagementApi.SignInExperience;

[JsonConverter(typeof(JsonStringEnumConverter<AgreeToTermsPolicy>))]
public enum AgreeToTermsPolicy { Automatic, ManualRegistrationOnly, Manual }

[JsonConverter(typeof(JsonStringEnumConverter<SignInMode>))]
public enum SignInMode { SignIn, Register, SignInAndRegister }

[JsonConverter(typeof(JsonStringEnumConverter<SignInIdentifier>))]
public enum SignInIdentifier
{
    [JsonStringEnumMemberName("username")] Username,
    [JsonStringEnumMemberName("email")] Email,
    [JsonStringEnumMemberName("phone")] Phone,
}

// Superset of SignInIdentifier — adds EmailOrPhone for secondary sign-up identifiers
[JsonConverter(typeof(JsonStringEnumConverter<SignUpIdentifier>))]
public enum SignUpIdentifier
{
    [JsonStringEnumMemberName("username")] Username,
    [JsonStringEnumMemberName("email")] Email,
    [JsonStringEnumMemberName("phone")] Phone,
    [JsonStringEnumMemberName("emailOrPhone")] EmailOrPhone,
}

[JsonConverter(typeof(JsonStringEnumConverter<MfaFactor>))]
public enum MfaFactor { Totp, WebAuthn, BackupCode, EmailVerificationCode, PhoneVerificationCode }

[JsonConverter(typeof(JsonStringEnumConverter<MfaPolicy>))]
public enum MfaPolicy
{
    UserControlled,
    Mandatory,
    PromptOnlyAtSignIn,
    PromptAtSignInAndSignUp,
    NoPrompt,
    PromptAtSignInAndSignUpMandatory,
    PromptOnlyAtSignInMandatory,
}

[JsonConverter(typeof(JsonStringEnumConverter<OrganizationMfaPolicy>))]
public enum OrganizationMfaPolicy { NoPrompt, Mandatory }

[JsonConverter(typeof(JsonStringEnumConverter<ForgotPasswordMethod>))]
public enum ForgotPasswordMethod { EmailVerificationCode, PhoneVerificationCode }

// --- Nested sub-records (prefixed "Sie" to avoid namespace collisions) ---

public record SieColor(string PrimaryColor, bool IsDarkModeEnabled, string DarkPrimaryColor);

public record SieBranding(
    string? LogoUrl = null,
    string? DarkLogoUrl = null,
    string? Favicon = null,
    string? DarkFavicon = null);

public record SieLanguageInfo(bool AutoDetect, string FallbackLanguage);

public record SieSignInMethod(
    SignInIdentifier Identifier,
    bool Password,
    bool VerificationCode,
    bool IsPasswordPrimary);

public record SieSignIn(IReadOnlyList<SieSignInMethod> Methods);

public record SieSecondaryIdentifier(SignUpIdentifier Identifier, bool? Verify = null);

public record SieSignUp(
    IReadOnlyList<SignInIdentifier> Identifiers,
    bool Password,
    bool Verify,
    IReadOnlyList<SieSecondaryIdentifier>? SecondaryIdentifiers = null);

public record SieSocialSignIn(bool? AutomaticAccountLinking = null, bool? SkipRequiredIdentifiers = null);

public record SieCustomUiAssets(string Id, double CreatedAt);

public record SieCustomUiCsp(
    IReadOnlyList<string>? ScriptSrc = null,
    IReadOnlyList<string>? ConnectSrc = null);

public record SiePasswordPolicyLength(double Min, double Max);

public record SiePasswordPolicyCharacterTypes(double Min);

public record SiePasswordPolicyRejects(
    bool Pwned,
    bool RepetitionAndSequence,
    bool UserInfo,
    IReadOnlyList<string> Words);

public record SiePasswordPolicy(
    SiePasswordPolicyLength? Length = null,
    SiePasswordPolicyCharacterTypes? CharacterTypes = null,
    SiePasswordPolicyRejects? Rejects = null);

public record SieMfa(
    IReadOnlyList<MfaFactor> Factors,
    MfaPolicy Policy,
    OrganizationMfaPolicy? OrganizationRequiredMfaPolicy = null);

public record SieAdaptiveMfa(bool? Enabled = null);

public record SieCaptchaPolicy(bool? Enabled = null);

public record SieSentinelPolicy(double? MaxAttempts = null, double? LockoutDuration = null);

public record SieEmailBlocklistPolicy(
    bool? BlockDisposableAddresses = null,
    bool? BlockSubaddressing = null,
    IReadOnlyList<string>? CustomBlocklist = null);

public record SieVerificationCodePolicy(double? ExpirationDuration = null, double? MaxRetryAttempts = null);

public record SieSignUpProfileField(string Name);

// oneOf {enabled:false} | {enabled:true, validPeriodDays, enabledAt?} — flattened
public record SiePasswordExpiration(bool? Enabled = null, double? ValidPeriodDays = null, double? EnabledAt = null);

public record SiePasskeySignIn(bool? Enabled = null, bool? ShowPasskeyButton = null, bool? AllowAutofill = null);

// --- Main response type ---

public record SignInExperience
{
    public required string TenantId { get; init; }
    public required string Id { get; init; }
    public required SieColor Color { get; init; }
    public SieBranding? Branding { get; init; }
    public bool HideLogtoBranding { get; init; }
    public required SieLanguageInfo LanguageInfo { get; init; }
    public string? TermsOfUseUrl { get; init; }
    public string? PrivacyPolicyUrl { get; init; }
    public AgreeToTermsPolicy AgreeToTermsPolicy { get; init; }
    public required SieSignIn SignIn { get; init; }
    public required SieSignUp SignUp { get; init; }
    public SieSocialSignIn? SocialSignIn { get; init; }
    public IReadOnlyList<string> SocialSignInConnectorTargets { get; init; } = [];
    public SignInMode SignInMode { get; init; }
    public string? CustomCss { get; init; }
    public Dictionary<string, string>? CustomContent { get; init; }
    public SieCustomUiAssets? CustomUiAssets { get; init; }
    public SieCustomUiCsp? CustomUiCsp { get; init; }
    public SiePasswordPolicy? PasswordPolicy { get; init; }
    public SieMfa? Mfa { get; init; }
    public SieAdaptiveMfa? AdaptiveMfa { get; init; }
    public bool SingleSignOnEnabled { get; init; }
    public string? SupportEmail { get; init; }
    public string? SupportWebsiteUrl { get; init; }
    public string? UnknownSessionRedirectUrl { get; init; }
    public SieCaptchaPolicy? CaptchaPolicy { get; init; }
    public SieSentinelPolicy? SentinelPolicy { get; init; }
    public SieEmailBlocklistPolicy? EmailBlocklistPolicy { get; init; }
    public SieVerificationCodePolicy? VerificationCodePolicy { get; init; }
    public IReadOnlyList<ForgotPasswordMethod>? ForgotPasswordMethods { get; init; }
    public SiePasskeySignIn? PasskeySignIn { get; init; }
    public IReadOnlyList<SieSignUpProfileField>? SignUpProfileFields { get; init; }
    public SiePasswordExpiration? PasswordExpiration { get; init; }
    public JsonElement? UsernamePolicy { get; init; }
}

// --- Request types ---

public record UpdateSignInExperienceRequest
{
    public SieColor? Color { get; init; }
    public SieBranding? Branding { get; init; }
    public bool? HideLogtoBranding { get; init; }
    public SieLanguageInfo? LanguageInfo { get; init; }
    public string? TermsOfUseUrl { get; init; }
    public string? PrivacyPolicyUrl { get; init; }
    public AgreeToTermsPolicy? AgreeToTermsPolicy { get; init; }
    public SieSignIn? SignIn { get; init; }
    public SieSignUp? SignUp { get; init; }
    public SieSocialSignIn? SocialSignIn { get; init; }
    public IReadOnlyList<string>? SocialSignInConnectorTargets { get; init; }
    public SignInMode? SignInMode { get; init; }
    public string? CustomCss { get; init; }
    public Dictionary<string, string>? CustomContent { get; init; }
    public SieCustomUiAssets? CustomUiAssets { get; init; }
    public SieCustomUiCsp? CustomUiCsp { get; init; }
    public SiePasswordPolicy? PasswordPolicy { get; init; }
    public SieMfa? Mfa { get; init; }
    public SieAdaptiveMfa? AdaptiveMfa { get; init; }
    public bool? SingleSignOnEnabled { get; init; }
    public string? SupportEmail { get; init; }
    public string? SupportWebsiteUrl { get; init; }
    public string? UnknownSessionRedirectUrl { get; init; }
    public SieCaptchaPolicy? CaptchaPolicy { get; init; }
    public SieSentinelPolicy? SentinelPolicy { get; init; }
    public SieEmailBlocklistPolicy? EmailBlocklistPolicy { get; init; }
    public SieVerificationCodePolicy? VerificationCodePolicy { get; init; }
    public IReadOnlyList<ForgotPasswordMethod>? ForgotPasswordMethods { get; init; }
    public SiePasskeySignIn? PasskeySignIn { get; init; }
    public IReadOnlyList<SieSignUpProfileField>? SignUpProfileFields { get; init; }
    public SiePasswordExpiration? PasswordExpiration { get; init; }
}

public record CheckPasswordRequest(string Password, string? UserId = null);

public record PasswordIssue(string Code, Dictionary<string, object>? Interpolation = null);

public record CheckPasswordResult(bool Result, IReadOnlyList<PasswordIssue>? Issues = null);

public record UploadCustomUiAssetsResult(string CustomUiAssetId);

public record UsernameCaseSensitivityConflictSample(string UsernameLower, IReadOnlyList<string> UserIds);

public record UsernameCaseSensitivityConflicts(
    double TotalConflicts,
    IReadOnlyList<UsernameCaseSensitivityConflictSample> Samples);

public record ApplicationSignInExperience(
    string TenantId,
    string ApplicationId,
    SieColor? Color,
    SieBranding? Branding,
    string? CustomCss,
    string? TermsOfUseUrl,
    string? PrivacyPolicyUrl,
    string? DisplayName);

public record ReplaceApplicationSignInExperienceRequest(
    string? TermsOfUseUrl,
    string? PrivacyPolicyUrl,
    SieColor? Color = null,
    SieBranding? Branding = null,
    string? CustomCss = null,
    string? DisplayName = null);
