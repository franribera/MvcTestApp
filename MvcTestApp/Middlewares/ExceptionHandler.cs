using System;
using System.IO;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Http;
using MvcTestApp.Components;
using Newtonsoft.Json;

namespace MvcTestApp.Middlewares
{
    public class ExceptionHandler
    {
        private readonly RequestDelegate _next;
        private readonly IContentTypeResolver _contentTypeResolver;
        private readonly IExceptionToHttpStatusCodeParser _exceptionToHttpStatusCodeParser;

        public class Message
        {
            public string Error { get; set; }
        }

        public ExceptionHandler(RequestDelegate next, IContentTypeResolver contentTypeResolver, IExceptionToHttpStatusCodeParser exceptionToHttpStatusCodeParser)
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

            context.Response.ContentType = contentType;
            context.Response.StatusCode = (int)_exceptionToHttpStatusCodeParser.Parse(exception);
            await context.Response.WriteAsync(SerializeError(contentType, exception.Message));
        }

        // This logic could be extracted in a new component, but since it is only used here i think it is not needed
        private static string SerializeError(string contentType, string exceptionMessage)
        {
            var message = new Message { Error = exceptionMessage };
            if (contentType == ContentType.ApplicationXml)
            {
                var xmlSerializer = new XmlSerializer(typeof(Message));
                using (var stringWriter = new StringWriter())
                using (var xmlWriter = XmlWriter.Create(stringWriter))
                {
                    xmlSerializer.Serialize(xmlWriter, message);
                    return stringWriter.ToString();
                }
            }

            return JsonConvert.SerializeObject(message);
        }
    }
}
