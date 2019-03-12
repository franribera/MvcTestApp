using MvcTestApp.Common.Serializers;

namespace MvcTestApp.Http
{
    public class ApplicationJsonContentType : ApplicationContentType
    {
        public ApplicationJsonContentType(IJsonSerializer jsonSerializer) : base(ContentType.ApplicationJson, jsonSerializer)
        {
        }
    }
}