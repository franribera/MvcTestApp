namespace MvcTestApp.Domain.Infrastructure
{
    public interface IRepository<TAggregate> where TAggregate : Aggregate
    {
        IUnitOfWork UnitOfWork { get; }
    }
}