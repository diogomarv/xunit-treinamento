using FluentValidation.Results;
using Newtonsoft.Json;
using System;

namespace Core.CartaoDeCredito.Domain.Dto
{
    public class SolicitacaoCartaoDeCreditoRequest
    {
        public string Nome { get; }
        public string Cpf { get; }
        public string Rg { get; }
        public string Profissao { get; }
        public decimal Renda { get; }
        public string NomeNoCartao { get; }

        [JsonConstructor]
        public SolicitacaoCartaoDeCreditoRequest(string nome, string cpf, string rg, string profissao, decimal renda, string nomeNoCartao)
        {
            Nome = nome;
            Cpf = cpf;
            Rg = rg;
            Profissao = profissao;
            Renda = renda;
            NomeNoCartao = nomeNoCartao;
        }

    }

    public class SolicitacaoCartaoDeCreditoResponse
    {
        public Guid? Id { get; }
        public string NumeroDoCartao { get; }
        public string Cvv { get; }
        public string DataDeValidade { get; }
        public string NomeNoCartao { get; }

        public ValidationResult Validation { get; }


        public SolicitacaoCartaoDeCreditoResponse(Guid? id, string numeroCartao, string cvv, string dataDeValidade, string nomeNoCartao, ValidationResult validation)
        {
            Id = id;
            Validation = validation;
            NumeroDoCartao = numeroCartao;
            Cvv = cvv;
            DataDeValidade = dataDeValidade;
            NomeNoCartao = nomeNoCartao;
        }

    }

    public static class SolicitacaoCartaoDeCreditoRequestResponseMappingExtension
    {
        public static SolicitacaoCartaoDeCredito ToDomain(this SolicitacaoCartaoDeCreditoRequest solicitacaoCartaoDeCredito)
        {
            return new SolicitacaoCartaoDeCredito(
                    solicitacaoCartaoDeCredito.Nome,
                    solicitacaoCartaoDeCredito.Cpf,
                    solicitacaoCartaoDeCredito.Rg,
                    solicitacaoCartaoDeCredito.Profissao,
                    solicitacaoCartaoDeCredito.Renda,
                    solicitacaoCartaoDeCredito.NomeNoCartao
               );
        }

        public static SolicitacaoCartaoDeCreditoResponse ToResponse(this SolicitacaoCartaoDeCredito solicitacaoCartaoDeCredito, CriarSolicitacaoAdquirenteResponse criarSolicitacaoAdquirenteResponse)
        {
            return new SolicitacaoCartaoDeCreditoResponse(
                    solicitacaoCartaoDeCredito.Id,
                    criarSolicitacaoAdquirenteResponse?.NumeroDoCartao,
                    criarSolicitacaoAdquirenteResponse?.Cvv,
                    criarSolicitacaoAdquirenteResponse?.DataDeValidade?.ToString("MM/yy"),
                    criarSolicitacaoAdquirenteResponse?.NomeNoCartao,
                    solicitacaoCartaoDeCredito.ValidationResult
                );
        }
    }
}
