using System.Linq;
using Microsoft.AspNetCore.Http;

namespace MvcTestApp.Components
{
    public class ContentTypeResolver : IContentTypeResolver
    {
        public string Resolve(IHeaderDictionary headerDictionary)
        {
            var headers = headerDictionary ?? new HeaderDictionary();

            return headers[Headers.Accept].Any() ? headers[Headers.Accept].First() : ContentType.ApplicationJson;
        }
    }
}