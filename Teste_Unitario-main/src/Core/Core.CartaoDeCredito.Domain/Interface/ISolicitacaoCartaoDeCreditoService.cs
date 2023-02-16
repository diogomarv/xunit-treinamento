using Core.CartaoDeCredito.Domain.Dto;

namespace Core.CartaoDeCredito.Domain.Interface
{
    public interface ISolicitacaoCartaoDeCreditoService
    {
        public SolicitacaoCartaoDeCreditoResponse SolicitarCartao(SolicitacaoCartaoDeCreditoRequest solicitacaoCartaoDeCreditoRequest);
        public bool VerificarCpfJaCadastrado(string cpf);
    }
}
