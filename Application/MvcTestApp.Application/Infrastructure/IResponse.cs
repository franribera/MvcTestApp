using System.Collections.Generic;
using MvcTestApp.Domain.Infrastructure;

namespace MvcTestApp.Application.Infrastructure
{
    public interface IResponse<out TEntity> where TEntity : Entity
    {
        bool SuccessFul { get; }
        string Message { get; }
        TEntity Entity { get; }
        IEnumerable<string> Errors { get; }
    }
}