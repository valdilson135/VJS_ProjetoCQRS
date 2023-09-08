﻿using Vjs.NegocioCore.Communication.Mediator;
using Vjs.NegocioCore.DomainObjects.DTO;
using Vjs.NegocioCore.Messages.CommonMessages.IntegrationEvents;
using Vjs.NegocioCore.Messages.CommonMessages.Notifications;

namespace Vjs.Pagamentos.Business
{
    public class PagamentoService : IPagamentoService
    {
        private readonly IPagamentoCartaoCreditoFacade _pagamentoCartaoCreditoFacade;
        private readonly IPagamentoRepository _pagamentoRepository;
        private readonly IMediatorHandler _mediatorHandler;

        public PagamentoService(IPagamentoCartaoCreditoFacade pagamentoCartaoCreditoFacade,
                                IPagamentoRepository pagamentoRepository, 
                                IMediatorHandler mediatorHandler)
        {
            _pagamentoCartaoCreditoFacade = pagamentoCartaoCreditoFacade;
            _pagamentoRepository = pagamentoRepository;
            _mediatorHandler = mediatorHandler;
        }

        public async Task<Transacao> RealizarPagamentoPedido(PagamentoPedido pagamentoPedido)
        {
            var pedido = new Pedido
            {
                Id = pagamentoPedido.PedidoId,
                Valor = pagamentoPedido.Total
            };

            var pagamento = new Pagamento
            {
                Valor = pagamentoPedido.Total,
                NomeCartao = pagamentoPedido.NomeCartao,
                NumeroCartao = pagamentoPedido.NumeroCartao,
                ExpiracaoCartao = pagamentoPedido.ExpiracaoCartao,
                CvvCartao = pagamentoPedido.CvvCartao,
                PedidoId = pagamentoPedido.PedidoId
            };

            var transacao = _pagamentoCartaoCreditoFacade.RealizarPagamento(pedido, pagamento);

            if (transacao.StatusTransacao == StatusTransacao.Pago)
            {
                pagamento.AdicionarEvento(new PedidoPagamentoRealizadoEvent(pedido.Id, pagamentoPedido.ClienteId, transacao.PagamentoId, transacao.Id, pedido.Valor));

                _pagamentoRepository.Adicionar(pagamento);
                _pagamentoRepository.AdicionarTransacao(transacao);

                await _pagamentoRepository.UnitOfWork.Commit();
                return transacao;
            }

            await _mediatorHandler.PublicarNotificacao(new DomainNotification("pagamento","A operadora recusou o pagamento"));
            await _mediatorHandler.PublicarEvento(new PedidoPagamentoRecusadoEvent(pedido.Id, pagamentoPedido.ClienteId, transacao.PagamentoId, transacao.Id, pedido.Valor));

            return transacao;
        }
    }
}