using EventSourcing;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Vjs.Catalogo.Application.Services;
using Vjs.Catalogo.DataRepository;
using Vjs.Catalogo.DataRepository.Repository;
using Vjs.Catalogo.Domain;
using Vjs.Catalogo.Domain.Events;
using Vjs.NegocioCore.Communication.Mediator;
using Vjs.NegocioCore.DataRepository.EventSourcing;
using Vjs.NegocioCore.Messages.CommonMessages.IntegrationEvents;
using Vjs.NegocioCore.Messages.CommonMessages.Notifications;
using Vjs.Pagamentos.AntiCorruption;
using Vjs.Pagamentos.Business;
using Vjs.Pagamentos.Business.Events;
using Vjs.Pagamentos.DataRepository;
using Vjs.Vendas.Application.Commands;
using Vjs.Vendas.Application.Events;
using Vjs.Vendas.Application.Queries;
using Vjs.Vendas.DataRepository;
using Vjs.Vendas.DataRepository.Repository;
using Vjs.Vendas.Domain;

namespace Vjs.MVC.Setup
{
    public static class DependencyInjection
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            // Mediator
            services.AddScoped<IMediatorHandler, MediatorHandler>();

            // Notifications
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

            // Event Sourcing
            services.AddSingleton<IEventStoreService, EventStoreService>();
            services.AddSingleton<IEventSourcingRepository, EventSourcingRepository>();

            // Catalogo
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<IProdutoAppService, ProdutoAppService>();
            services.AddScoped<IEstoqueService, EstoqueService>();
            services.AddScoped<CatalogoContext>();

            services.AddScoped<INotificationHandler<ProdutoAbaixoEstoqueEvent>, ProdutoEventHandler>();
            services.AddScoped<INotificationHandler<PedidoIniciadoEvent>, ProdutoEventHandler>();
            services.AddScoped<INotificationHandler<PedidoProcessamentoCanceladoEvent>, ProdutoEventHandler>();

            // Vendas
            services.AddScoped<IPedidoRepository, PedidoRepository>();
            services.AddScoped<IPedidoQueries, PedidoQueries>();
            services.AddScoped<VendasContext>();

            services.AddScoped<IRequestHandler<AdicionarItemPedidoCommand, bool>, PedidoCommandHandler>();
            services.AddScoped<IRequestHandler<AtualizarItemPedidoCommand, bool>, PedidoCommandHandler>();
            services.AddScoped<IRequestHandler<RemoverItemPedidoCommand, bool>, PedidoCommandHandler>();
            services.AddScoped<IRequestHandler<AplicarVoucherPedidoCommand, bool>, PedidoCommandHandler>();
            services.AddScoped<IRequestHandler<IniciarPedidoCommand, bool>, PedidoCommandHandler>();
            services.AddScoped<IRequestHandler<FinalizarPedidoCommand, bool>, PedidoCommandHandler>();
            services.AddScoped<IRequestHandler<CancelarProcessamentoPedidoCommand, bool>, PedidoCommandHandler>();
            services.AddScoped<IRequestHandler<CancelarProcessamentoPedidoEstornarEstoqueCommand, bool>, PedidoCommandHandler>();

            services.AddScoped<INotificationHandler<PedidoRascunhoIniciadoEvent>, PedidoEventHandler>();
            services.AddScoped<INotificationHandler<PedidoEstoqueRejeitadoEvent>, PedidoEventHandler>();
            services.AddScoped<INotificationHandler<PedidoPagamentoRealizadoEvent>, PedidoEventHandler>();
            services.AddScoped<INotificationHandler<PedidoPagamentoRecusadoEvent>, PedidoEventHandler>();

            // Pagamento
            services.AddScoped<IPagamentoRepository, PagamentoRepository>();
            services.AddScoped<IPagamentoService, PagamentoService>();
            services.AddScoped<IPagamentoCartaoCreditoFacade, PagamentoCartaoCreditoFacade>();
            services.AddScoped<IPayPalGateway, PayPalGateway>();
            services.AddScoped<IConfigurationManager, ConfigurationManagerConfig>();
            services.AddScoped<PagamentoContext>();

            services.AddScoped<INotificationHandler<PedidoEstoqueConfirmadoEvent>, PagamentoEventHandler>();
            
        }
    }
}