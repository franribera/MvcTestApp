using System.IO;
using System.Xml;

namespace MvcTestApp.Common.Serializers
{
    public class XmlSerializer : IXmlSerializer
    {
        public string Serialize<TType>(TType value) where TType : class
        {
            var xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(TType));
            using (var stringWriter = new StringWriter())
            using (var xmlWriter = XmlWriter.Create(stringWriter))
            {
                xmlSerializer.Serialize(xmlWriter, value);
                return stringWriter.ToString();
            }
        }
    }
}