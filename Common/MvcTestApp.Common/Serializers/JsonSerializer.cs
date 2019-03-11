using Newtonsoft.Json;

namespace MvcTestApp.Common.Serializers
{
    public class JsonSerializer : ISerializer
    {
        public string Serialize<TType>(TType value) where TType : class
        {
            return JsonConvert.SerializeObject(value);
        }
    }
}