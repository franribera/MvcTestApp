using System.Threading.Tasks;

namespace MvcTestApp.Application.Infrastructure
{
    public interface IInputPort<in TRequest, out TResponse>
    {
        Task Handle(TRequest request, IOutputPort<TResponse> outputPort);
    }
}