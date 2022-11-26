using System.Net;

namespace ExceptionHandling;

public sealed record ErrorResult
{
    public HttpStatusCode Code { get; init; }
    public string? TraceId { get; init; }
    public IDictionary<string, string[]> Errors { get; init; } = default!;
}