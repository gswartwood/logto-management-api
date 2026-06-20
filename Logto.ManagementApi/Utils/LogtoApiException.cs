using System.Net;

namespace Logto.ManagementApi.Utils;

public class LogtoApiException(HttpStatusCode statusCode, string message) : Exception(message)
{
    public HttpStatusCode StatusCode { get; } = statusCode;
}

public sealed class LogtoBadRequestException(string message)
    : LogtoApiException(HttpStatusCode.BadRequest, message);

public sealed class LogtoUnauthorizedException(string message)
    : LogtoApiException(HttpStatusCode.Unauthorized, message);

public sealed class LogtoForbiddenException(string message)
    : LogtoApiException(HttpStatusCode.Forbidden, message);

public sealed class LogtoNotFoundException(string message)
    : LogtoApiException(HttpStatusCode.NotFound, message);

public sealed class LogtoUnprocessableEntityException(string message)
    : LogtoApiException(HttpStatusCode.UnprocessableEntity, message);
