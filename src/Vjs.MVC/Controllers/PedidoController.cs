using MediatR;
using Microsoft.AspNetCore.Mvc;
using Vjs.NegocioCore.Communication.Mediator;
using Vjs.NegocioCore.Messages.CommonMessages.Notifications;
using Vjs.Vendas.Application.Queries;

namespace Vjs.MVC.Controllers
{
    public class PedidoController : ControllerBase
    {
        private readonly IPedidoQueries _pedidoQueries;

        public PedidoController(IPedidoQueries pedidoQueries,
            INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediatorHandler) : base(notifications, mediatorHandler)
        {
            _pedidoQueries = pedidoQueries;
        }

        [Route("meus-pedidos")]
        public async Task<IActionResult> Index()
        {
            return View(await _pedidoQueries.ObterPedidosCliente(ClienteId));
        }
    }
}