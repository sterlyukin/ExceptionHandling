using System.Net;

namespace ExceptionHandling;

public sealed record ErrorResult
{
    public HttpStatusCode Code { get; init; }
    public string? TraceId { get; init; }
    public string Message { get; init; } = default!;
}