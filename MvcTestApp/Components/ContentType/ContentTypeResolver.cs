using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using MvcTestApp.Http;

namespace MvcTestApp.Components.ContentType
{
    public class ContentTypeResolver : IContentTypeResolver
    {
        private readonly IEnumerable<IApplicationContentType> _contentTypes;

        public ContentTypeResolver(IEnumerable<IApplicationContentType> contentTypes)
        {
            _contentTypes = contentTypes;
        }

        public IApplicationContentType Resolve(IHeaderDictionary headerDictionary)
        {
            var headers = headerDictionary ?? new HeaderDictionary();
            var contentType = headers[HeaderNames.Accept].Any() ? headers[HeaderNames.Accept].First() : Http.ContentType.ApplicationJson;

            return _contentTypes.Single(applicationContentType => applicationContentType.HeaderValue == contentType);
        }
    }
}