using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        public class Message
        {
            public string Error { get; set; }
        }

        public ExceptionHandler(RequestDelegate next)
        {
            _next = next;
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

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var contentType = context.Request.Headers[Headers.Accept].Any() ? context.Request.Headers[Headers.Accept].First() : "application/json";

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
        // Also the content-type concept could be extracted in a type to not have magic string, but here applies the same reason.
        private static string SerializeError(string contentType, string exceptionMessage)
        {
            var message = new Message { Error = exceptionMessage };
            if (contentType == "application/xml")
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
