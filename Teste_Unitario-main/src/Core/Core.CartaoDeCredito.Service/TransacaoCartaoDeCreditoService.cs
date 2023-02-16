using Core.CartaoDeCredito.Domain;
using Core.CartaoDeCredito.Domain.Dto;
using Core.CartaoDeCredito.Domain.Interface;
using FluentValidation;

namespace Core.CartaoDeCredito.Service
{
    public class TransacaoCartaoDeCreditoService : ITransacaoCartaoDeCreditoService
    {
        private readonly ITransacaoCartaoDeCreditoRepository _transacaoCartaoDeCreditoRepository;
        private readonly IValidator<TransacaoCartaoDeCredito> _transacaoCartaoDeCredito;

        public TransacaoCartaoDeCreditoService(ITransacaoCartaoDeCreditoRepository transacaoCartaoDeCreditoRepository, IValidator<TransacaoCartaoDeCredito> transacaoCartaoDeCredito)
        {
            _transacaoCartaoDeCreditoRepository = transacaoCartaoDeCreditoRepository;
            _transacaoCartaoDeCredito = transacaoCartaoDeCredito;
        }

        public TransacaoCartaoDeCreditoResponse Criar(TransacaoCartaoDeCreditoRequest transacaoRequest)
        {
            var transacao = transacaoRequest.ToDomain();
            transacao.Validate(_transacaoCartaoDeCredito);

            if (transacao.ValidationResult.IsValid)
                _transacaoCartaoDeCreditoRepository.Criar(transacao);

            transacao.ResultadoTransacao(transacao.ValidationResult.IsValid);

            return transacao.ToResponse();
        }
    }
}
