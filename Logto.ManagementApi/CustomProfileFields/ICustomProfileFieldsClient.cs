namespace Logto.ManagementApi.CustomProfileFields;

public interface ICustomProfileFieldsClient
{
    Task<IReadOnlyList<CustomProfileField>> ListAsync(CancellationToken cancellationToken = default);
    Task<CustomProfileField> CreateAsync(CreateCustomProfileFieldRequest request, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<CustomProfileField>> BatchCreateAsync(IReadOnlyList<CreateCustomProfileFieldRequest> requests, CancellationToken cancellationToken = default);
    Task<CustomProfileField> GetAsync(string name, CancellationToken cancellationToken = default);
    Task<CustomProfileField> UpdateAsync(string name, UpdateCustomProfileFieldRequest request, CancellationToken cancellationToken = default);
    Task DeleteAsync(string name, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<CustomProfileField>> UpdateSieOrderAsync(UpdateCustomProfileFieldsSieOrderRequest request, CancellationToken cancellationToken = default);
}
