using Core.CartaoDeCredito.Domain;
using Core.CartaoDeCredito.Domain.Dto;
using Core.CartaoDeCredito.Domain.Interface;

namespace Core.CartaoDeCredito.Repository
{
    public class SolicitacaoCartaoDeCreditoRepository : ISolicitacaoCartaoDeCreditoRepository
    {
        public CriarSolicitacaoAdquirenteResponse CriarSolicitacaoAdquirente(SolicitacaoCartaoDeCredito solicitacaoCartaoDeCredito)
        {
            throw new System.NotImplementedException();
        }

        public bool VerificarCpfJaCadastrado(string cpf)
        {
            throw new System.NotImplementedException();
        }
    }
}
