using Core.CartaoDeCredito.Domain.Dto;

namespace Core.CartaoDeCredito.Domain.Interface
{
    public interface ITransacaoCartaoDeCreditoService
    {
        TransacaoCartaoDeCreditoResponse Criar(TransacaoCartaoDeCreditoRequest transacao);
    }
}
