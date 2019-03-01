using System;
using System.Threading;
using System.Threading.Tasks;

namespace MvcTestApp.Domain.Infrastructure
{
    public interface IUnitOfWork : IDisposable
    {
        Task<bool> CommitAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}