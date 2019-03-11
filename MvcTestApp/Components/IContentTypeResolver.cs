using Microsoft.AspNetCore.Http;

namespace MvcTestApp.Components
{
    public interface IContentTypeResolver
    {
        string Resolve(IHeaderDictionary headerDictionary);
    }
}