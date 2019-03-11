using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MvcTestApp.Components;

namespace MvcTestApp.Tests.Components
{
    [TestClass]
    public class ContentTypeResolverTests
    {
        private ContentTypeResolver _contentTypeResolver;

        [TestInitialize]
        public void TestInitialize()
        {
            _contentTypeResolver = new ContentTypeResolver();
        }

        [TestMethod]
        public void Resolve_NullHeadersCollection_ReturnsJsonAsDefault()
        {
            TestResolve(null, ContentType.ApplicationJson);
        }

        [TestMethod]
        public void Resolve_EmptyHeadersCollection_ReturnsJsonAsDefault()
        {
            TestResolve(new Dictionary<string, StringValues>(), ContentType.ApplicationJson);
        }

        [TestMethod]
        public void Resolve_HeadersCollectionWithAnyAcceptedContentType_ReturnsTheContentTypeEquivalentToTheFirstOne()
        {
            const string expectedContentType = ContentType.ApplicationXml;

            var headers = new Dictionary<string, StringValues>
            {
                { Headers.Accept, new StringValues(new []{ expectedContentType, ContentType.ApplicationJson})}
            };

            TestResolve(headers, expectedContentType);
        }

        private void TestResolve(Dictionary<string, StringValues> headerDictionary, string expectedContentType)
        {
            // Arrange
            var headers = new HeaderDictionary(headerDictionary);

            // Act
            var contentType = _contentTypeResolver.Resolve(headers);

            // Assert
            Assert.AreEqual(expectedContentType, contentType);
        }
    }
}
