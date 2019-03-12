using System;
using System.Net;

namespace MvcTestApp.Components.Exceptions
{
    public interface IExceptionToHttpStatusCodeParser
    {
        HttpStatusCode Parse(Exception exception);
    }
}