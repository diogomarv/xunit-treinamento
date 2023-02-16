using Core.CartaoDeCredito.Domain.Interface;
using Core.CartaoDeCredito.Domain.Dto;
using FluentValidation;
using Core.CartaoDeCredito.Domain;

namespace Core.CartaoDeCredito.Service
{
    public class SolicitacaoCartaoDeCreditoService : ISolicitacaoCartaoDeCreditoService
    {
        private readonly ISolicitacaoCartaoDeCreditoRepository _solicitacaoCartaoDeCreditoRepository;
        private readonly IMesaDeCreditoService _mesaDeCreditoService;
        private readonly IValidator<SolicitacaoCartaoDeCredito> _solicitacaoCartaoDeCreditoValidator;

        public SolicitacaoCartaoDeCreditoService(ISolicitacaoCartaoDeCreditoRepository solicitacaoCartaoDeCreditoRepository, IMesaDeCreditoService mesaDeCreditoService, 
            IValidator<SolicitacaoCartaoDeCredito> solicitacaoCartaoDeCreditoValidator)
        {
            _solicitacaoCartaoDeCreditoRepository = solicitacaoCartaoDeCreditoRepository;
            _mesaDeCreditoService = mesaDeCreditoService;
            _solicitacaoCartaoDeCreditoValidator = solicitacaoCartaoDeCreditoValidator;
        }

        public SolicitacaoCartaoDeCreditoResponse SolicitarCartao(SolicitacaoCartaoDeCreditoRequest solicitacaoCartaoDeCreditoRequest)
        {
            var solicitacaoCartaoDeCredito = solicitacaoCartaoDeCreditoRequest.ToDomain();
            CriarSolicitacaoAdquirenteResponse criarSolicitacaoAdquirenteResponse = null;
            solicitacaoCartaoDeCredito.Validate(_solicitacaoCartaoDeCreditoValidator);

            if(solicitacaoCartaoDeCredito.ValidationResult.IsValid)
            {
                solicitacaoCartaoDeCredito.FoiEnviadoParaMesaDeCredito(_mesaDeCreditoService.EnviarParaMesaDeCredito(new MesaDeCreditoRequest(solicitacaoCartaoDeCredito)));
                criarSolicitacaoAdquirenteResponse = _solicitacaoCartaoDeCreditoRepository.CriarSolicitacaoAdquirente(solicitacaoCartaoDeCredito);
            }
            
            return solicitacaoCartaoDeCredito.ToResponse(criarSolicitacaoAdquirenteResponse);
        }

        public bool VerificarCpfJaCadastrado(string cpf)
        {
            return _solicitacaoCartaoDeCreditoRepository.VerificarCpfJaCadastrado(cpf);
        }
    }
}
