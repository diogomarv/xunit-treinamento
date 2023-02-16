using Core.CartaoDeCredito.Domain.Dto;

namespace Core.CartaoDeCredito.Domain.Interface
{
    public interface ISolicitacaoCartaoDeCreditoRepository
    {
        public bool VerificarCpfJaCadastrado(string cpf);
        public CriarSolicitacaoAdquirenteResponse CriarSolicitacaoAdquirente(SolicitacaoCartaoDeCredito solicitacaoCartaoDeCredito);
    }
}
