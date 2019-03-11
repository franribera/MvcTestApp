using System;
using System.Collections.Generic;
using System.Net;

namespace MvcTestApp.Components
{
    internal class ExceptionToHttpStatusCodeParser : IExceptionToHttpStatusCodeParser
    {
        public HttpStatusCode Parse(Exception exception)
        {
            switch (exception)
            {
                case ArgumentException _:
                    return HttpStatusCode.BadRequest;
                case KeyNotFoundException _:
                    return HttpStatusCode.NotFound;
                case InvalidOperationException _:
                    return HttpStatusCode.Conflict;
                default:
                    return HttpStatusCode.InternalServerError;
            }
        }
    }
}