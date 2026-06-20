using System.Text.Json;
using System.Text.Json.Serialization;

namespace Logto.ManagementApi.Verifications;

// Shared

public record VerificationIdentifier(string Type, string Value);

public record VerificationRecord(string VerificationRecordId, string ExpiresAt);

public record VerificationRecordResult(string VerificationRecordId);

// Verification codes (/api/verification-codes)

public record CreateVerificationCodeRequest(
    string? Email = null,
    string? Phone = null
);

public record VerifyVerificationCodeRequest(
    string VerificationCode,
    string? Email = null,
    string? Phone = null
);

// Verifications — password

public record CreatePasswordVerificationRequest(string Password);

// Verifications — verification code

public record CreateVerificationCodeVerificationRequest(
    VerificationIdentifier Identifier,
    string? TemplateType = null
);

public record VerifyVerificationCodeVerificationRequest(
    VerificationIdentifier Identifier,
    string VerificationId,
    string Code
);

// Verifications — social

public record CreateSocialVerificationRequest(
    string State,
    string RedirectUri,
    string ConnectorId,
    string? Scope = null
);

public record SocialVerificationResult(
    string VerificationRecordId,
    string AuthorizationUri,
    string ExpiresAt
);

public record VerifySocialVerificationRequest(
    Dictionary<string, object> ConnectorData,
    string VerificationRecordId,
    JsonElement? VerificationId = null
);

// Verifications — WebAuthn

public record WebAuthnRegistrationResult(
    string VerificationRecordId,
    JsonElement RegistrationOptions,
    string ExpiresAt
);

public record WebAuthnRegistrationResponse(
    string ClientDataJSON,
    string AttestationObject,
    string? AuthenticatorData = null,
    IReadOnlyList<string>? Transports = null,
    double? PublicKeyAlgorithm = null,
    string? PublicKey = null
);

public record WebAuthnClientExtensionResults(
    bool? Appid = null,
    JsonElement? CrepProps = null,
    bool? HmacCreateSecret = null
);

public record WebAuthnRegistrationPayload(
    string Type,
    string Id,
    string RawId,
    WebAuthnRegistrationResponse Response,
    WebAuthnClientExtensionResults ClientExtensionResults,
    string? AuthenticatorAttachment = null
);

public record VerifyWebAuthnRegistrationRequest(
    string VerificationRecordId,
    WebAuthnRegistrationPayload Payload
);
