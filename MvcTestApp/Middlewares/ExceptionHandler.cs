using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
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

        public class Message
        {
            public string Error { get; set; }
        }

        public ExceptionHandler(RequestDelegate next, IContentTypeResolver contentTypeResolver)
        {
            _next = next;
            _contentTypeResolver = contentTypeResolver;
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
            context.Response.StatusCode = ParseStatusCode(exception);
            await context.Response.WriteAsync(SerializeError(contentType, exception.Message));
        }

        private static int ParseStatusCode(Exception exception)
        {
            switch (exception)
            {
                case ArgumentException _:
                    return (int)HttpStatusCode.BadRequest;
                case KeyNotFoundException _:
                    return (int)HttpStatusCode.NotFound;
                case InvalidOperationException _:
                    return (int)HttpStatusCode.Conflict;
                default:
                    return (int)HttpStatusCode.InternalServerError;
            }
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
