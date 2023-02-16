namespace Core.CartaoDeCredito.Domain.Interface
{
    public interface ITransacaoCartaoDeCreditoRepository
    {
        bool Criar(TransacaoCartaoDeCredito transacao);
    }
}
