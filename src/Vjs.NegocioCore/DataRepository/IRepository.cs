using Vjs.NegocioCore.DomainObjects;

namespace Vjs.NegocioCore.DataRepository
{
    public interface IRepository<T> : IDisposable where T : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}