using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using MvcTestApp.Components;
using MvcTestApp.Components.ContentType;
using MvcTestApp.Components.Exceptions;

namespace MvcTestApp.Middlewares
{
    public class ExceptionHandler
    {
        private readonly RequestDelegate _next;
        private readonly IContentTypeResolver _contentTypeResolver;
        private readonly IExceptionToHttpStatusCodeParser _exceptionToHttpStatusCodeParser;

        public ExceptionHandler(RequestDelegate next, IContentTypeResolver contentTypeResolver,
            IExceptionToHttpStatusCodeParser exceptionToHttpStatusCodeParser)
        {
            _next = next;
            _contentTypeResolver = contentTypeResolver;
            _exceptionToHttpStatusCodeParser = exceptionToHttpStatusCodeParser;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var contentType = _contentTypeResolver.Resolve(context.Request.Headers);

            context.Response.ContentType = contentType.HeaderValue;
            context.Response.StatusCode = (int)_exceptionToHttpStatusCodeParser.Parse(exception);
            await context.Response.WriteAsync(contentType.Serialize(new { Error = exception.Message}));
        }
    }
}
