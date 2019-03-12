using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
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
            var contentType = headers[Headers.Accept].Any() ? headers[Headers.Accept].First() : Http.ContentType.ApplicationJson;

            return _contentTypes.Single(applicationContentType => applicationContentType.HeaderValue == contentType);
        }
    }
}