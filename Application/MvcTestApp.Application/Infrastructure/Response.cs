using System.Collections.Generic;
using MvcTestApp.Domain.Infrastructure;

namespace MvcTestApp.Application.Infrastructure
{
    public class Response<TEntity> : IResponse<TEntity> where TEntity : Entity
    {
        public bool SuccessFul { get; }
        public string Message { get; }
        public TEntity Entity { get; }
        public IEnumerable<string> Errors { get; }

        protected Response(bool successful, TEntity entity, string message, IEnumerable<string> errors)
        {
            SuccessFul = successful;
            Message = message;
            Entity = entity;
            Errors = errors;
        }

        public static Response<TEntity> Success(TEntity entity, string message = null)
        {
            return new Response<TEntity>(true, entity, message, new List<string>());
        }

        public static Response<TEntity> Fail(IEnumerable<string> errors)
        {
            return new Response<TEntity>(false, null, null, errors);
        }
    }
}