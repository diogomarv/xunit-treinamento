namespace Core.CartaoDeCredito.Domain.Dto
{
    public class MesaDeCreditoRequest
    {
        public string Nome { get; }
        public string Cpf { get; }
        public string Rg { get; }
        public string Profissao { get; }
        public decimal Renda { get; }
        public string NomeNoCartao { get; }

        public MesaDeCreditoRequest(SolicitacaoCartaoDeCredito solicitacaoCartaoDeCredito)
        {
            if (solicitacaoCartaoDeCredito is null)
                throw new System.ArgumentNullException(nameof(solicitacaoCartaoDeCredito));

            Nome = solicitacaoCartaoDeCredito.Nome;
            Cpf = solicitacaoCartaoDeCredito.Cpf;
            Rg = solicitacaoCartaoDeCredito.Rg;
            Profissao = solicitacaoCartaoDeCredito.Profissao;
            Renda = solicitacaoCartaoDeCredito.Renda;
            NomeNoCartao = solicitacaoCartaoDeCredito.NomeNoCartao;
        }
    }
}
