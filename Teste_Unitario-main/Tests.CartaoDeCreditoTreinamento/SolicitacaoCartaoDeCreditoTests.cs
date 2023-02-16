using Core.CartaoDeCredito.Domain;
using Core.CartaoDeCredito.Domain.Interface;
using FluentValidation;
using Moq;
using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xunit;

namespace Tests.CartaoDeCreditoTreinamento
{
    public class UsuarioService
    {
        public async Task<string> ObterUsuarioPorId(int id)
        {

            await Task.Delay(2000); // 2 segundos

            return "Vit�o";
        }
    }

    public class SolicitacaoCartaoDeCreditoTests
    {

        private readonly Mock<ISolicitacaoCartaoDeCreditoRepository> _solicitacaoCartaoDeCreditoRepository;

        // Inicializar uma inst�ncia do Mock
        public SolicitacaoCartaoDeCreditoTests()
        {
            _solicitacaoCartaoDeCreditoRepository = new Mock<ISolicitacaoCartaoDeCreditoRepository>();
        }

        // Solicitar cart�o de cr�dito com dados v�lidos
        // Given_When_Then
        // Dado_Quando_Entao
        [Fact(DisplayName = "Solicitar cart�o de cr�dito com dados v�lidos")]
        [Trait("Categoria", "Cart�o de Cr�dito - Solicita��o")]
        public void CartaoDeCredito_SolicitarCartaoComDadosValidos_DeveCriarSolicitacaoComSucesso()
        {

            // Arrange (Arranjo / Arranjando / Preparando o objeto)
            IValidator<SolicitacaoCartaoDeCredito> _solicitacaoCartaoDeCreditoValidator = new SolicitacaoCartaoDeCreditoValidator(_solicitacaoCartaoDeCreditoRepository.Object);

            var solicitacaoCartaoDeCredito = new SolicitacaoCartaoDeCredito("Reginaldo Caumo", "01234567890", "01234567890", "CEO", 70000, "R�gis");

            // Act (Agir / Agindo)
            solicitacaoCartaoDeCredito.Validate(_solicitacaoCartaoDeCreditoValidator);

            bool resultado = solicitacaoCartaoDeCredito.ValidationResult.IsValid;

            // Assert (Meter bala no teste)
            Assert.True(resultado);

        }

        // Given_When_Then
        // Dado_QUando_Entao
        [Theory]
        [InlineData("", "01234567890", "01234567890", "Autonomo", 7000, "")]
        public void CartaoDeCredito_SolicitarCartaoComDadosInvalidos_NaoDeveCriarSolicitacaoComSucesso(string nome, string cpf, string rg, string profissao, decimal renda, string nomeNoCartao)
        {

            // Arrange
            _solicitacaoCartaoDeCreditoRepository.Setup(x => x.VerificarCpfJaCadastrado(It.Is<string>(y => y == "01234567890"))).Returns(false);

            IValidator<SolicitacaoCartaoDeCredito> _solicitacaoCartaoDeCreditoValidator = new SolicitacaoCartaoDeCreditoValidator(_solicitacaoCartaoDeCreditoRepository.Object);

            var solicitacaoCartaoDeCredito = new SolicitacaoCartaoDeCredito(nome, cpf, rg, profissao, renda, nomeNoCartao);

            // Act

            solicitacaoCartaoDeCredito.Validate(_solicitacaoCartaoDeCreditoValidator);

            bool resultadoRetornado = solicitacaoCartaoDeCredito.ValidationResult.IsValid;

            // Assert
            Assert.False(resultadoRetornado);

        }

        // Solicitar cart�o de cr�dito com renda acima de 800 reais. Deve retornar cart�o esperado
        // Given_When_Then
        // Dado_Quando_Entao
        [Theory(DisplayName = "Solicitar cart�o de cr�dito com renda acima de 800 reais. Deve retornar cart�o esperado")]
        [Trait("Categoria", "Cart�o de Cr�dito - Solicitacao")]
        [InlineData(2500, TipoCartao.Platinum)]
        [InlineData(700, TipoCartao.Gold)]
        public void CartaoDeCredito_SolicitarCartaoComRendaMaior800_DeveRetornarCartaoEsperado(decimal renda, TipoCartao? tipoCartaoEsperado)
        {
            // Arrange
            _solicitacaoCartaoDeCreditoRepository.Setup(x => x.VerificarCpfJaCadastrado(It.Is<string>(y => y == "01234567890"))).Returns(false);

            IValidator<SolicitacaoCartaoDeCredito> _solicitacaoCartaoDeCreditoValidator = new SolicitacaoCartaoDeCreditoValidator(_solicitacaoCartaoDeCreditoRepository.Object);

            var solicitarCartaoDeCredito = new SolicitacaoCartaoDeCredito("Diogo", "12345678900", "12345678900", "Programador Boss", 600, "Dio");

            // Act
            solicitarCartaoDeCredito.Validate(_solicitacaoCartaoDeCreditoValidator);

            // Assert
            Assert.Equal(tipoCartaoEsperado, solicitarCartaoDeCredito.TipoCartaoDisponivel);

        }

        // Given_When_Then
        [Fact]
        public async void Usuario_ObterUsuarioPorId_RetornarOVitao()
        {
            UsuarioService usuario = new();

            string resultadoEsperado = "Vit�o";
            string resultado = await usuario.ObterUsuarioPorId(1);

            Assert.Equal(resultadoEsperado, resultado);
        }

    }
}
