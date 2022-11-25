using System.Net;

namespace ExceptionHandling;

public sealed record ErrorResult
{
    public HttpStatusCode Code { get; init; }
    public string? TraceId { get; internal init; }
    public IDictionary<string, string[]> Errors { get; internal init; } = default!;
}