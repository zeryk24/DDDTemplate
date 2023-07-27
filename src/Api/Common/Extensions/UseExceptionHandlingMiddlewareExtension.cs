using Presentation.Common.Middlewares;

namespace Presentation.Common.Extensions;

public static class UseExceptionHandlingMiddlewareExtension
{
    public static void UseExceptionHanlingMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionHandlingMiddleware>();
    }
}
