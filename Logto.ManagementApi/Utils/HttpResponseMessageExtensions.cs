using System.Net;

namespace Logto.ManagementApi.Utils;

internal static class HttpResponseMessageExtensions
{
    internal static async Task ThrowIfErrorAsync(this HttpResponseMessage response, CancellationToken cancellationToken)
    {
        if (response.IsSuccessStatusCode) return;

        var body = await response.Content.ReadAsStringAsync(cancellationToken);
        throw response.StatusCode switch
        {
            HttpStatusCode.BadRequest => new LogtoBadRequestException(body),
            HttpStatusCode.Unauthorized => new LogtoUnauthorizedException(body),
            HttpStatusCode.Forbidden => new LogtoForbiddenException(body),
            HttpStatusCode.NotFound => new LogtoNotFoundException(body),
            HttpStatusCode.UnprocessableEntity => new LogtoUnprocessableEntityException(body),
            _ => new LogtoApiException(response.StatusCode, body),
        };
    }
}
