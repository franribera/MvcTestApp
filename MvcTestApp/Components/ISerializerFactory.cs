using MvcTestApp.Common.Serializers;

namespace MvcTestApp.Components
{
    public interface ISerializerFactory
    {
        ISerializer Create(string contentType);
    }
}