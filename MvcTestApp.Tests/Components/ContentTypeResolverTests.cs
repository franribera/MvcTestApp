using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MvcTestApp.Common.Serializers;
using MvcTestApp.Components;
using MvcTestApp.Components.ContentType;
using MvcTestApp.Http;

namespace MvcTestApp.Tests.Components
{
    [TestClass]
    public class ContentTypeResolverTests
    {
        private Mock<IJsonSerializer> _jsonSerializerMock;
        private Mock<IXmlSerializer> _xmlSerializerMock;

        [TestInitialize]
        public void TestInitialize()
        {
            _jsonSerializerMock = new Mock<IJsonSerializer>();
            _xmlSerializerMock = new Mock<IXmlSerializer>();
        }

        [TestMethod]
        public void Resolve_NullHeadersCollection_ReturnsJsonAsDefault()
        {
            // Arrange
            var contentTypes = new[] {new ApplicationJsonContentType(_jsonSerializerMock.Object)};
            var contentTypeResolver = new ContentTypeResolver(contentTypes);

            // Act
            var contentType = contentTypeResolver.Resolve(null);

            // Assert
            Assert.AreEqual(ContentType.ApplicationJson, contentType.HeaderValue);
        }

        [TestMethod]
        public void Resolve_EmptyHeadersCollection_ReturnsJsonAsDefault()
        {
            // Arrange
            var contentTypes = new[] { new ApplicationJsonContentType(_jsonSerializerMock.Object) };
            var contentTypeResolver = new ContentTypeResolver(contentTypes);

            // Act
            var contentType = contentTypeResolver.Resolve(new HeaderDictionary());

            // Assert
            Assert.AreEqual(ContentType.ApplicationJson, contentType.HeaderValue);
        }

        [TestMethod]
        public void Resolve_HeadersCollectionWithAnyAcceptedContentType_ReturnsTheContentTypeEquivalentToTheFirstOne()
        {
            // Arrange
            const string expectedContentType = ContentType.ApplicationXml;
            var headers = new Dictionary<string, StringValues>
            {
                { Headers.Accept, new StringValues(new []{ expectedContentType, ContentType.ApplicationJson})}
            };

            var contentTypes = new IApplicationContentType[]
            {
                new ApplicationXmlContentType(_xmlSerializerMock.Object), 
                new ApplicationJsonContentType(_jsonSerializerMock.Object)
            };

            var contentTypeResolver = new ContentTypeResolver(contentTypes);

            // Act
            var contentType = contentTypeResolver.Resolve(new HeaderDictionary(headers));

            // Assert
            Assert.AreEqual(expectedContentType, contentType.HeaderValue);
        }
    }
}
