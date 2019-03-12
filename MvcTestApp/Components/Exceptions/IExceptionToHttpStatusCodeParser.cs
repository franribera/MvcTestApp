using System;
using System.Net;

namespace MvcTestApp.Components
{
    public interface IExceptionToHttpStatusCodeParser
    {
        HttpStatusCode Parse(Exception exception);
    }
}