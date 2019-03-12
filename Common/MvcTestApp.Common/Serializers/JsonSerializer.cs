using Newtonsoft.Json;

namespace MvcTestApp.Common.Serializers
{
    public class JsonSerializer : IJsonSerializer
    {
        public string Serialize<TType>(TType value) where TType : class
        {
            return JsonConvert.SerializeObject(value);
        }
    }
}