using MvcTestApp.Common.Serializers;

namespace MvcTestApp.Http
{
    public abstract class ApplicationContentType : IApplicationContentType
    {
        private readonly ISerializer _serializer;

        public string HeaderValue { get; }

        protected ApplicationContentType(string headerValue, ISerializer serializer)
        {
            HeaderValue = headerValue;
            _serializer = serializer;
        }

        public string Serialize(object value)
        {
            return _serializer.Serialize(value);
        }
    }
}