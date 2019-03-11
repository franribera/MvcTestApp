using MvcTestApp.Common.Serializers;

namespace MvcTestApp.Components
{
    public class SerializerFactory : ISerializerFactory
    {
        public ISerializer Create(string contentType)
        {
            switch (contentType)
            {
                case ContentType.ApplicationXml:
                    return new XmlSerializer();
                default:
                    return new JsonSerializer();
            }
        }
    }
}