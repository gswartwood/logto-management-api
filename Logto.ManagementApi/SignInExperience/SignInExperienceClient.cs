using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using Logto.ManagementApi.Utils;

namespace Logto.ManagementApi.SignInExperience;

public sealed class SignInExperienceClient(HttpClient httpClient) : ISignInExperienceClient
{
    private static readonly JsonSerializerOptions WriteOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
    };

    public async Task<SignInExperience> GetAsync(CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync("sign-in-exp", cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<SignInExperience>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for sign-in-exp get.");
    }

    public async Task<SignInExperience> UpdateAsync(UpdateSignInExperienceRequest request, bool? removeUnusedDemoSocialConnector = null, CancellationToken cancellationToken = default)
    {
        var url = removeUnusedDemoSocialConnector is not null
            ? $"sign-in-exp?removeUnusedDemoSocialConnector={removeUnusedDemoSocialConnector.Value.ToString().ToLowerInvariant()}"
            : "sign-in-exp";
        var response = await httpClient.PatchAsJsonAsync(url, request, WriteOptions, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<SignInExperience>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for sign-in-exp update.");
    }

    public async Task<CheckPasswordResult> CheckPasswordAsync(CheckPasswordRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync("sign-in-exp/default/check-password", request, WriteOptions, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<CheckPasswordResult>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for sign-in-exp check-password.");
    }

    public async Task<UploadCustomUiAssetsResult> UploadCustomUiAssetsAsync(Stream file, string fileName, CancellationToken cancellationToken = default)
    {
        using var content = new MultipartFormDataContent();
        content.Add(new StreamContent(file), "file", fileName);
        var response = await httpClient.PostAsync("sign-in-exp/default/custom-ui-assets", content, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<UploadCustomUiAssetsResult>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for sign-in-exp custom-ui-assets upload.");
    }

    public async Task<UsernameCaseSensitivityConflicts> GetUsernameCaseSensitivityConflictsAsync(int limit, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync($"sign-in-exp/username-policy/case-sensitivity-conflicts?limit={limit}", cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<UsernameCaseSensitivityConflicts>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for sign-in-exp username case-sensitivity-conflicts.");
    }

    public async Task<ApplicationSignInExperience> GetApplicationSignInExperienceAsync(string applicationId, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync($"applications/{Uri.EscapeDataString(applicationId)}/sign-in-experience", cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<ApplicationSignInExperience>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for application sign-in-experience get.");
    }

    public async Task<ApplicationSignInExperience> ReplaceApplicationSignInExperienceAsync(string applicationId, ReplaceApplicationSignInExperienceRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PutAsJsonAsync($"applications/{Uri.EscapeDataString(applicationId)}/sign-in-experience", request, WriteOptions, cancellationToken);
        await response.ThrowIfErrorAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<ApplicationSignInExperience>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Logto returned an empty response for application sign-in-experience replace.");
    }
}
