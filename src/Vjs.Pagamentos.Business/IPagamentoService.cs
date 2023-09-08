using Vjs.NegocioCore.DomainObjects.DTO;

namespace Vjs.Pagamentos.Business
{
    public interface IPagamentoService
    {
        Task<Transacao> RealizarPagamentoPedido(PagamentoPedido pagamentoPedido);
    }
}