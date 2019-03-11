using MvcTestApp.Common.Serializers;

namespace MvcTestApp.Components
{
    internal class SerializerFactory : ISerializerFactory
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