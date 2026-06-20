namespace Logto.ManagementApi.Users;

public interface IUserAssetsClient
{
    Task<UserAssetServiceStatus> GetServiceStatusAsync(CancellationToken cancellationToken = default);
    Task<UserAsset> UploadAsync(Stream file, string fileName, string? contentType = null, CancellationToken cancellationToken = default);
}
