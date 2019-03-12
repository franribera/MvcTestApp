using MvcTestApp.Common.Serializers;

namespace MvcTestApp.Http
{
    public class ApplicationXmlContentType : ApplicationContentType
    {
        public ApplicationXmlContentType(IXmlSerializer xmlSerializer) : base(ContentType.ApplicationXml, xmlSerializer)
        {
        }
    }
}