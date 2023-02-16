using Core.CartaoDeCredito.Domain.Dto;

namespace Core.CartaoDeCredito.Domain.Interface
{
    public interface IMesaDeCreditoService
    {
        bool EnviarParaMesaDeCredito(MesaDeCreditoRequest solicitacaoCartaoDeCredito);
    }
}
