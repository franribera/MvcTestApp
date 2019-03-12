using Microsoft.AspNetCore.Http;
using MvcTestApp.Http;

namespace MvcTestApp.Components.ContentType
{
    public interface IContentTypeResolver
    {
        IApplicationContentType Resolve(IHeaderDictionary headerDictionary);
    }
}