namespace MvcTestApp.Common.Serializers
{
    public interface ISerializer
    {
        string Serialize<TType>(TType value) where TType : class;
    }
}
