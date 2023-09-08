using Vjs.NegocioCore.Messages;
using Vjs.NegocioCore.Messages.CommonMessages.DomainEvents;
using Vjs.NegocioCore.Messages.CommonMessages.Notifications;

namespace Vjs.NegocioCore.Communication.Mediator
{
    public interface IMediatorHandler
    {
        Task PublicarEvento<T>(T evento) where T : Event;
        Task<bool> EnviarComando<T>(T comando) where T : Command;
        Task PublicarNotificacao<T>(T notificacao) where T : DomainNotification;
        Task PublicarDomainEvent<T>(T notificacao) where T : DomainEvent;
    }
}
