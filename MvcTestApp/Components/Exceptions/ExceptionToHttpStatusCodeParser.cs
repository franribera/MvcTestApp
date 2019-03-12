using System;
using System.Collections.Generic;
using System.Net;

namespace MvcTestApp.Components.Exceptions
{
    public class ExceptionToHttpStatusCodeParser : IExceptionToHttpStatusCodeParser
    {
        // Reading these key-value pairs from a configuration source (config file, database, ....) will
        // avoid break the OCP. 
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