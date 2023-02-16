using Core.CartaoDeCredito.Domain.Interface;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;

namespace Core.CartaoDeCredito.Domain
{
    public class SolicitacaoCartaoDeCredito
    {
        public static string ERRO_RENDA = "Renda abaixo de R$:800,00";

        public ValidationResult ValidationResult { get; private set; }
        public Guid? Id { get; }
        public string Nome { get; }
        public string Cpf { get; }
        public string Rg { get; }
        public string Profissao { get; }
        public decimal Renda { get; }
        public string NomeNoCartao { get; }
        public Enum TipoCartaoDisponivel { get; }
        public bool EnviadoParaMesaDeCredito { get; private set; }

        public SolicitacaoCartaoDeCredito(string nome,
            string cpf,
            string rg,
            string profissao,
            decimal renda,
            string nomeNoCartao,
            Guid? id = default)
        {
            Nome = nome;
            Cpf = cpf;
            Rg = rg;
            Profissao = profissao;
            Renda = renda;
            NomeNoCartao = nomeNoCartao;
            TipoCartaoDisponivel = TipoDeCartaoPorRenda(renda);
            Id = id ?? Guid.NewGuid();
        }

        private Enum TipoDeCartaoPorRenda(decimal renda)
        {
            if (renda >= 2500m)
                return TipoCartao.Platinum;
            else if (renda >= 800m)
                return TipoCartao.Gold;

            throw new DomainException(ERRO_RENDA);
        }

        public void FoiEnviadoParaMesaDeCredito(bool enviadoParaMesaDeCredito) =>
            EnviadoParaMesaDeCredito = enviadoParaMesaDeCredito;

        public ValidationResult Validate(IValidator<SolicitacaoCartaoDeCredito> solicitacaoCartaoDeCreditoValidator)
        {
            ValidationResult = solicitacaoCartaoDeCreditoValidator.Validate(this);

            return ValidationResult;
        }
    }

    public class SolicitacaoCartaoDeCreditoValidator : AbstractValidator<SolicitacaoCartaoDeCredito>
    {
        private static IReadOnlyDictionary<string, string> _erroMsg => new Dictionary<string, string>()
        {
            {"erro_nome", "Insira um nome válido"},
            {"erro_cpf", "Insira um cpf válido"},
            {"erro_cpf_cadastrado", "Cpf já cadastrado"},
            {"erro_rg", "Insira um RG válido"},
            {"erro_profissao", "Insira uma profissão válida"},
            {"erro_nome_cartao", "Insira um nome no cartão válido"},
        };

        public static IReadOnlyDictionary<string, string> Erro_Msg => _erroMsg;

        public SolicitacaoCartaoDeCreditoValidator(ISolicitacaoCartaoDeCreditoRepository solicitacaoCartaoDeCreditoRepository)
        {
            RuleFor(s => s.Nome)
                .NotEmpty()
                .WithMessage(Erro_Msg["erro_nome"]);

            RuleFor(s => s.Cpf)
                .NotEmpty()
                .WithMessage(Erro_Msg["erro_cpf"])
                .Must(cpf => !solicitacaoCartaoDeCreditoRepository.VerificarCpfJaCadastrado(cpf))
                .WithMessage(Erro_Msg["erro_cpf_cadastrado"]);

            RuleFor(s => s.Rg)
                .NotEmpty()
                .WithMessage(Erro_Msg["erro_rg"]);

            RuleFor(s => s.Profissao)
                .NotEmpty()
                .WithMessage(Erro_Msg["erro_profissao"]);

            RuleFor(s => s.NomeNoCartao)
                .NotEmpty()
                .WithMessage(Erro_Msg["erro_nome_cartao"]);
        }
    }
}
