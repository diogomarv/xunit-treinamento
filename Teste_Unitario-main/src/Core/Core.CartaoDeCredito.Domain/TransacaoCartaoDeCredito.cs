using FluentValidation;
using FluentValidation.Results;
using System;

namespace Core.CartaoDeCredito.Domain
{
    public class TransacaoCartaoDeCredito
    {
        public Guid Id { get; set; }
        public CartaoDeCredito CartaoDeCredito { get; }
        public decimal ValorTotal { get; }
        public StatusTransacao StatusTransacao { get; private set; }
        public ValidationResult ValidationResult { get; private set; }

        public TransacaoCartaoDeCredito(CartaoDeCredito cartaoDeCredito, decimal valorTotal, Guid? id = default)
        {
            CartaoDeCredito = cartaoDeCredito;
            ValorTotal = valorTotal;
            Id = id ?? Guid.NewGuid();
        }

        public ValidationResult Validate(IValidator<TransacaoCartaoDeCredito> transacaoCartaoDeCreditoValidator)
        {
            ValidationResult = transacaoCartaoDeCreditoValidator.Validate(this);

            return ValidationResult;
        }

        public void ResultadoTransacao(bool resultadoTransacao)
        {
            if (resultadoTransacao)
                StatusTransacao = StatusTransacao.Aprovada;
            else
                StatusTransacao = StatusTransacao.Reprovada;
        }
    }

    public class TransacaoCartaoDeCreditoValidator : AbstractValidator<TransacaoCartaoDeCredito>
    {
        public TransacaoCartaoDeCreditoValidator()
        {
            RuleFor(t => t.ValorTotal)
                .GreaterThan(0)
                .WithMessage("Valor total inválido");

            RuleFor(c => c.CartaoDeCredito)
                .SetValidator(new CartaoDeCreditoValidator());
        }
    }
}
