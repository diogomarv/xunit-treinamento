using Core.CartaoDeCredito.Domain;
using Core.CartaoDeCredito.Domain.Interface;
using FluentValidation;
using Moq;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Tests.CartaoDeCredito.Domain
{

    public class SolicitacaoCartaoDeCreditoTests
    {
        private readonly Mock<ISolicitacaoCartaoDeCreditoService> _solicitacaoCartaoDeCreditoService;
        private readonly Mock<ISolicitacaoCartaoDeCreditoRepository> _solicitacaoCartaoDeCreditoRepository;

        public SolicitacaoCartaoDeCreditoTests()
        {
            _solicitacaoCartaoDeCreditoService = new Mock<ISolicitacaoCartaoDeCreditoService>();
            _solicitacaoCartaoDeCreditoRepository = new Mock<ISolicitacaoCartaoDeCreditoRepository>();
        }

        [Fact(DisplayName = "Solicitar cartão com dados válidos")]
        [Trait("Categoria", "Cartão de Crédito - Solicitação")]
        public void CartaoDeCredito_SolicitarCartaoComDadosValidos_DeveCriarSolicitacaoComSucesso()
        {
            //Arrange
            IValidator<SolicitacaoCartaoDeCredito> _solicitacaoCartaoDeCreditoValidator = new SolicitacaoCartaoDeCreditoValidator(_solicitacaoCartaoDeCreditoRepository.Object);

            // Act
            var solicitacaoCartaoDeCredito = new SolicitacaoCartaoDeCredito("Teste Teste",
                "01234567890",
                "1234567890",
                "Analista de Sistemas",
                2000m,
                "Teste Plástico");
            solicitacaoCartaoDeCredito.Validate(_solicitacaoCartaoDeCreditoValidator);

            //Assert
            Assert.True(solicitacaoCartaoDeCredito.ValidationResult.IsValid);
        }

        [Theory(DisplayName = "Solicitar cartão com dados inválidos")]
        [Trait("Categoria", "Cartão de Crédito - Solicitação")]
        [InlineData("", "01234567890", "123456789", "Analista de Sistemas", 2000, "Teste Plástico", "erro_nome")]
        [InlineData("Teste Nome", "", "123456789", "Analista de Sistemas", 2000, "Teste Plástico", "erro_cpf")]
        [InlineData("Teste Nome", "98765432100", "123456789", "Analista de Sistemas", 2000, "Teste Plástico", "erro_cpf_cadastrado")]
        [InlineData("Teste Nome", "01234567890", "", "Analista de Sistemas", 2000, "Teste Plástico", "erro_rg")]
        [InlineData("Teste Nome", "01234567890", "123456789", "", 2000, "Teste Plástico", "erro_profissao")]
        [InlineData("Teste Nome", "01234567890", "123456789", "Analista de Sistemas", 2000, "", "erro_nome_cartao")]
        public void CartaoDeCredito_SolicitarCartaoComDadosInvalidos_NaoDeveCriarSolicitacaoComSucesso(string nome,
            string cpf,
            string rg,
            string profissao,
            decimal renda,
            string nomeNoCartao,
            string mensagem)
        {
            //Arrange, Act
            _solicitacaoCartaoDeCreditoRepository.Setup(x => x.VerificarCpfJaCadastrado(It.Is<string>(x => x == "98765432100")))
                                              .Returns(false);

            IValidator<SolicitacaoCartaoDeCredito> _solicitacaoCartaoDeCreditoValidator = new SolicitacaoCartaoDeCreditoValidator(_solicitacaoCartaoDeCreditoRepository.Object);

            var solicitacaoCartaoDeCredito = new SolicitacaoCartaoDeCredito(nome,
                cpf,
                rg,
                profissao,
                renda,
                nomeNoCartao);
            solicitacaoCartaoDeCredito.Validate(_solicitacaoCartaoDeCreditoValidator);

            //Assert
            Assert.False(solicitacaoCartaoDeCredito.ValidationResult.IsValid);
            Assert.Contains(SolicitacaoCartaoDeCreditoValidator.Erro_Msg[mensagem], solicitacaoCartaoDeCredito.ValidationResult.Errors.Select(e => e.ErrorMessage));
        }

        [Theory(DisplayName = "Solicitar cartão de crédito com renda acima de 800 reais deve retornar cartão esperado")]
        [Trait("Categoria", "Cartão de Crédito - Solicitação")]
        [InlineData(800, TipoCartao.Gold)]
        [InlineData(2000, TipoCartao.Gold)]
        [InlineData(2500, TipoCartao.Platinum)]
        [InlineData(2600, TipoCartao.Platinum)]
        public void CartaoDeCredito_SolicitarCartaoComRendaMaiorQue800_DeveRetornarCartaoEsperado(decimal renda, TipoCartao? tipoCartaoEsperado)
        {
            //Arrange, Act
            _solicitacaoCartaoDeCreditoService.Setup(x => x.VerificarCpfJaCadastrado(It.Is<string>(x => x == "01234567890")))
                                              .Returns(false);
            IValidator<SolicitacaoCartaoDeCredito> _solicitacaoCartaoDeCreditoValidator = new SolicitacaoCartaoDeCreditoValidator(_solicitacaoCartaoDeCreditoRepository.Object);

            var solicitacaoCartaoDeCredito = new SolicitacaoCartaoDeCredito("Teste Teste",
                "01234567890",
                "1234567890",
                "Analista de Sistemas",
                renda,
                "Teste Plástico");
            solicitacaoCartaoDeCredito.Validate(_solicitacaoCartaoDeCreditoValidator);

            //Assert
            Assert.Equal(tipoCartaoEsperado, solicitacaoCartaoDeCredito.TipoCartaoDisponivel);
        }

        [Fact(DisplayName = "Solicitar cartão de crédito com renda abaixo de 800 reais deve retornar erro")]
        [Trait("Categoria", "Cartão de Crédito - Solicitação")]
        public void CartaoDeCredito_SolicitarCartaoComRendaMenorQue800_DeveRetornarErro()
        {
            //Arrange, Act, Assert
            var exception = Assert.Throws<DomainException>(() => new SolicitacaoCartaoDeCredito("Teste Teste",
                "01234567890",
                "1234567890",
                "Analista de Sistemas",
                799,
                "Teste Plástico"));

            Assert.Equal(SolicitacaoCartaoDeCredito.ERRO_RENDA, exception.Message);
        }

    }
}
