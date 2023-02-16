using FluentValidation;
using System;
using System.Globalization;

namespace Core.CartaoDeCredito.Domain
{
    public class CartaoDeCredito
    {
        public string NomeNoCartao { get; }
        public string NumeroCartaoVirtual { get; }
        public string Cvv { get; }
        public string DataDeValidade { get; }
        public string Cpf { get; }

        public CartaoDeCredito(string cpf, string cvv, string dataDeValidade, string nomeNoCartao, string numeroCartaoVirtual)
        {
            NomeNoCartao = nomeNoCartao;
            NumeroCartaoVirtual = numeroCartaoVirtual;
            Cvv = cvv;
            DataDeValidade = dataDeValidade;
            Cpf = cpf;
        }
    }

    public class CartaoDeCreditoValidator : AbstractValidator<CartaoDeCredito>
    {
        public CartaoDeCreditoValidator()
        {
            RuleFor(c => c.NomeNoCartao)
                .NotEmpty()
                .WithMessage("Nome no Cartão inválido");

            RuleFor(c => c.NumeroCartaoVirtual)
                .NotEmpty()
                .WithMessage("Número do cartão inválido");

            RuleFor(c => c.Cvv)
                .NotEmpty()
                .WithMessage("Número do cvv inválido");

            RuleFor(c => c.DataDeValidade)
                .NotEmpty()
                .WithMessage("Data de validade inválida")
                .Must(c => DateTime.TryParseExact(c, "MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var result) && result.Date > DateTime.Now.Date)
                .WithMessage("Cartão vencido");

            RuleFor(c => c.Cpf)
                .NotEmpty()
                .WithMessage("CPF inválido");
        }
    }
}
