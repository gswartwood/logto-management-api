namespace Logto.ManagementApi.SignInExperience;

public interface ISignInExperienceClient
{
    Task<SignInExperience> GetAsync(CancellationToken cancellationToken = default);
    Task<SignInExperience> UpdateAsync(UpdateSignInExperienceRequest request, bool? removeUnusedDemoSocialConnector = null, CancellationToken cancellationToken = default);
    Task<CheckPasswordResult> CheckPasswordAsync(CheckPasswordRequest request, CancellationToken cancellationToken = default);
    Task<UploadCustomUiAssetsResult> UploadCustomUiAssetsAsync(Stream file, string fileName, CancellationToken cancellationToken = default);
    Task<UsernameCaseSensitivityConflicts> GetUsernameCaseSensitivityConflictsAsync(int limit, CancellationToken cancellationToken = default);
    Task<ApplicationSignInExperience> GetApplicationSignInExperienceAsync(string applicationId, CancellationToken cancellationToken = default);
    Task<ApplicationSignInExperience> ReplaceApplicationSignInExperienceAsync(string applicationId, ReplaceApplicationSignInExperienceRequest request, CancellationToken cancellationToken = default);
}
