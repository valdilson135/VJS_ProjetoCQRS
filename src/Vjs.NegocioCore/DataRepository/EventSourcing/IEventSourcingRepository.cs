using Vjs.NegocioCore.Messages;
namespace Vjs.NegocioCore.DataRepository.EventSourcing
{
    public interface IEventSourcingRepository
    {
        Task SalvarEvento<TEvent>(TEvent evento) where TEvent : Event;
        Task<IEnumerable<StoredEvent>> ObterEventos(Guid aggregateId);
    }
}