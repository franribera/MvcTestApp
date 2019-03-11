using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using MvcTestApp.Components;

namespace MvcTestApp.Middlewares
{
    public class ExceptionHandler
    {
        private readonly RequestDelegate _next;
        private readonly IContentTypeResolver _contentTypeResolver;
        private readonly IExceptionToHttpStatusCodeParser _exceptionToHttpStatusCodeParser;
        private readonly ISerializerFactory _serializerFactory;

        public ExceptionHandler(RequestDelegate next, IContentTypeResolver contentTypeResolver,
            IExceptionToHttpStatusCodeParser exceptionToHttpStatusCodeParser, ISerializerFactory serializerFactory)
        {
            _next = next;
            _contentTypeResolver = contentTypeResolver;
            _exceptionToHttpStatusCodeParser = exceptionToHttpStatusCodeParser;
            _serializerFactory = serializerFactory;
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

            context.Response.ContentType = contentType;
            context.Response.StatusCode = (int)_exceptionToHttpStatusCodeParser.Parse(exception);
            await context.Response.WriteAsync(SerializeError(contentType, exception.Message));
        }

        private string SerializeError(string contentType, string exceptionMessage)
        {
            var message = new { Error = exceptionMessage };
            var serializer = _serializerFactory.Create(contentType);
            return serializer.Serialize(message);
        }
    }
}
