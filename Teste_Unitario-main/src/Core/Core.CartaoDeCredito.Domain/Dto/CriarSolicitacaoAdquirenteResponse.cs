using System;

namespace Core.CartaoDeCredito.Domain.Dto
{
    public class CriarSolicitacaoAdquirenteResponse
    {
        public string NumeroDoCartao { get; }
        public string Cvv { get; }
        public DateTime? DataDeValidade { get; }
        public string NomeNoCartao { get; }

        public CriarSolicitacaoAdquirenteResponse(string numeroCartao, string cvv, DateTime dataDeValidade, string nomeNoCartao)
        {
            NumeroDoCartao = numeroCartao;
            Cvv = cvv;
            DataDeValidade = dataDeValidade;
            NomeNoCartao = nomeNoCartao;
        }
    }
}
