using event_scheduler.api.Mapping;
using Microsoft.AspNetCore.Diagnostics;

namespace event_scheduler.api.Exceptions;

public class GlobalExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        await httpContext.Response.WriteAsJsonAsync(
            new GlobalResponse<Exception>(false, "internal server error", errors: [exception.Message])
        );
        return true;
    }
}