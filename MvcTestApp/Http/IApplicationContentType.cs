namespace MvcTestApp.Http
{
    public interface IApplicationContentType
    {
        string HeaderValue { get; }
        string Serialize(object value);
    }
}