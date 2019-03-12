using Microsoft.AspNetCore.Http;
using MvcTestApp.Http;

namespace MvcTestApp.Components
{
    public interface IContentTypeResolver
    {
        IApplicationContentType Resolve(IHeaderDictionary headerDictionary);
    }
}