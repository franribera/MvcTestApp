namespace MvcTestApp.Application.Infrastructure
{
    public interface IOutputPort<in TResponse>
    {
        void Handle(TResponse response);
    }
}