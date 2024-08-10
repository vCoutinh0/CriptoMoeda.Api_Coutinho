using Domain.Adapters;
using Domain.Exceptions;
using Domain.Models;
using Domain.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Xunit;
using Assert = Xunit.Assert;

namespace Application.Tests
{
    [TestClass]
    public class CriptoMoedaTests
    {
        private readonly ICriptoMoedaService criptoMoedaService;
        private readonly Mock<IMercadoBitcoinAdapter> mercadoBitcoinAdapterMock;
        private readonly Mock<INegociacoesDbRepositoryAdapter> negociacoesDbRepositoryAdapterMock;
        private readonly Mock<INegociacoesHistoricoDbRepositoryAdapter> negociacoesHistoricoDbRepositoryAdapterMock;

        public CriptoMoedaTests()
        {
            mercadoBitcoinAdapterMock = new Mock<IMercadoBitcoinAdapter>();
            negociacoesDbRepositoryAdapterMock = new Mock<INegociacoesDbRepositoryAdapter>();
            negociacoesHistoricoDbRepositoryAdapterMock = new Mock<INegociacoesHistoricoDbRepositoryAdapter>();
            criptoMoedaService = new CriptoMoedaService(mercadoBitcoinAdapterMock.Object, negociacoesDbRepositoryAdapterMock.Object, negociacoesHistoricoDbRepositoryAdapterMock.Object);
        }

        [TestMethod]
        [Trait(nameof(ICriptoMoedaService.ObterDadosNegociacoesDoDiaAsync), "Sucesso")]
        public async Task ObterDadosNegociacoesDoDiaAsync_Sucesso()
        {
            var siglaCripto = "xpto";

            var retornoAtualizarIntegracao = new NegociacoesDoDia()
            {
                DataHora = DateTime.Parse("2010-10-10"),
                MaiorPreco = 10,
                MenorPreco = 5,
                MaiorPrecoOfertado = 11,
                MenorPrecoOfertado = 4,
                PrecoUnitario = 10,
                QuantidadeNegociada = 100
            };

            mercadoBitcoinAdapterMock
                .Setup(m => m.ObterDadosNegociacoesDoDiaAsync(It.IsAny<string>()))
                .Callback<string>(
                (sigla) =>
                {
                    Assert.Equal("xpto", sigla);
                })
                .ReturnsAsync(retornoAtualizarIntegracao);


            await criptoMoedaService.ObterDadosNegociacoesDoDiaAsync(siglaCripto);

            mercadoBitcoinAdapterMock.Verify(s => s.
            ObterDadosNegociacoesDoDiaAsync(It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        [Trait(nameof(ICriptoMoedaService.ObterDadosNegociacoesDoDiaAsync), "Erro")]
        public async Task ObterDadosNegociacoesDoDiaAsync_Erro()
        {
            await Xunit.Assert.ThrowsAnyAsync<Exception>(async () =>
            {
                await criptoMoedaService.ObterDadosNegociacoesDoDiaAsync(string.Empty);
            });
        }

        [TestMethod]
        [Trait(nameof(ICriptoMoedaService.ObterHistoricoNegociacoesAsync), "Sucesso")]
        public async Task ObterHistoricoNegociacoesExistenteAsync_Sucesso()
        {
            var siglaCripto = "xpto";

            var retornoNegociacoesDoDia = new NegociacoesDoDia()
            {
                DataHora = DateTime.Parse("2010-10-10"),
                MaiorPreco = 10,
                MenorPreco = 5,
                MaiorPrecoOfertado = 11,
                MenorPrecoOfertado = 4,
                PrecoUnitario = 10,
                QuantidadeNegociada = 100
            };

            var retornoNegociacoesExistente = new NegociacoesDoDia()
            {
                DataHora = DateTime.Parse("2010-10-09"),
                MaiorPreco = 11,
                MenorPreco = 6,
                MaiorPrecoOfertado = 9,
                MenorPrecoOfertado = 4,
                PrecoUnitario = 10,
                QuantidadeNegociada = 100
            };

            IEnumerable<NegociacoesDoDia> returnoHistoricoNegociacoes = 
                new List<NegociacoesDoDia>() { 
                    retornoNegociacoesExistente,
                    retornoNegociacoesDoDia };

            mercadoBitcoinAdapterMock
                .Setup(m => m.ObterDadosNegociacoesDoDiaAsync(It.IsAny<string>()))
                .Callback<string>(
                (sigla) =>
                {
                    Assert.Equal("xpto", sigla);
                })
                .ReturnsAsync(retornoNegociacoesDoDia);

            negociacoesDbRepositoryAdapterMock
                .Setup(m => m.ObterAsync(It.IsAny<string>()))
                .Callback<string>(
                (sigla) =>
                {
                    Assert.Equal("xpto", sigla);
                })
                .ReturnsAsync(retornoNegociacoesExistente);

            negociacoesDbRepositoryAdapterMock
                .Setup(m => m.SalvarAsync(It.IsAny<NegociacoesDoDia>()))
                .Callback<NegociacoesDoDia>(
                (negociacoes) =>
                {
                    Assert.Equal(retornoNegociacoesDoDia, negociacoes);
                });

            negociacoesHistoricoDbRepositoryAdapterMock
                .Setup(m => m.RegistrarNegociacoesHistoricoAsync(It.IsAny<NegociacoesDoDia>()))
                .Callback<NegociacoesDoDia>(
                (negociacoes) =>
                {
                    Assert.Equal(retornoNegociacoesDoDia, negociacoes);
                });
            
            negociacoesHistoricoDbRepositoryAdapterMock
                .Setup(m => m.ObterHistoricoAsync(It.IsAny<string>()))
                .Callback<string>(
                (sigla) =>
                {
                    Assert.Equal("xpto", sigla);
                })
                .ReturnsAsync(returnoHistoricoNegociacoes);

            await criptoMoedaService.ObterHistoricoNegociacoesAsync(siglaCripto);

            mercadoBitcoinAdapterMock.Verify(s => s
                .ObterDadosNegociacoesDoDiaAsync(It.IsAny<string>()), Times.Once);

            negociacoesDbRepositoryAdapterMock.Verify(s => s
                .ObterAsync(It.IsAny<string>()), Times.Once);

            negociacoesDbRepositoryAdapterMock.Verify(s => s
                .AtualizarAsync(It.IsAny<NegociacoesDoDia>()), Times.Once);

            negociacoesDbRepositoryAdapterMock.Verify(s => s
                .SalvarAsync(It.IsAny<NegociacoesDoDia>()), Times.Never);

            negociacoesHistoricoDbRepositoryAdapterMock.Verify(s => s
                .RegistrarNegociacoesHistoricoAsync(It.IsAny<NegociacoesDoDia>()), Times.Once);

            negociacoesHistoricoDbRepositoryAdapterMock.Verify(s => s
                .ObterHistoricoAsync(It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        [Trait(nameof(ICriptoMoedaService.ObterHistoricoNegociacoesAsync), "Sucesso")]
        public async Task ObterHistoricoNegociacoesNaoExistenteAsync_Sucesso()
        {
            var siglaCripto = "xpto";

            var retornoNegociacoesDoDia = new NegociacoesDoDia()
            {
                DataHora = DateTime.Parse("2010-10-10"),
                MaiorPreco = 10,
                MenorPreco = 5,
                MaiorPrecoOfertado = 11,
                MenorPrecoOfertado = 4,
                PrecoUnitario = 10,
                QuantidadeNegociada = 100
            };

            IEnumerable<NegociacoesDoDia> returnoHistoricoNegociacoes =
                new List<NegociacoesDoDia>() {
                    retornoNegociacoesDoDia };

            mercadoBitcoinAdapterMock
                .Setup(m => m.ObterDadosNegociacoesDoDiaAsync(It.IsAny<string>()))
                .Callback<string>(
                (sigla) =>
                {
                    Assert.Equal("xpto", sigla);
                })
                .ReturnsAsync(retornoNegociacoesDoDia);

            negociacoesDbRepositoryAdapterMock
                .Setup(m => m.ObterAsync(It.IsAny<string>()))
                .Callback<string>(
                (sigla) =>
                {
                    Assert.Equal("xpto", sigla);
                });

            negociacoesDbRepositoryAdapterMock
                .Setup(m => m.AtualizarAsync(It.IsAny<NegociacoesDoDia>()))
                .Callback<NegociacoesDoDia>(
                (negociacoes) =>
                {
                    Assert.Equal(retornoNegociacoesDoDia, negociacoes);
                });

            negociacoesHistoricoDbRepositoryAdapterMock
                .Setup(m => m.RegistrarNegociacoesHistoricoAsync(It.IsAny<NegociacoesDoDia>()))
                .Callback<NegociacoesDoDia>(
                (negociacoes) =>
                {
                    Assert.Equal(retornoNegociacoesDoDia, negociacoes);
                });

            negociacoesHistoricoDbRepositoryAdapterMock
                .Setup(m => m.ObterHistoricoAsync(It.IsAny<string>()))
                .Callback<string>(
                (sigla) =>
                {
                    Assert.Equal("xpto", sigla);
                })
                .ReturnsAsync(returnoHistoricoNegociacoes);

            await criptoMoedaService.ObterHistoricoNegociacoesAsync(siglaCripto);

            mercadoBitcoinAdapterMock.Verify(s => s
                .ObterDadosNegociacoesDoDiaAsync(It.IsAny<string>()), Times.Once);

            negociacoesDbRepositoryAdapterMock.Verify(s => s
                .ObterAsync(It.IsAny<string>()), Times.Once);

            negociacoesDbRepositoryAdapterMock.Verify(s => s
                .AtualizarAsync(It.IsAny<NegociacoesDoDia>()), Times.Never);

            negociacoesDbRepositoryAdapterMock.Verify(s => s
                .SalvarAsync(It.IsAny<NegociacoesDoDia>()), Times.Once);

            negociacoesHistoricoDbRepositoryAdapterMock.Verify(s => s
                .RegistrarNegociacoesHistoricoAsync(It.IsAny<NegociacoesDoDia>()), Times.Once);

            negociacoesHistoricoDbRepositoryAdapterMock.Verify(s => s
                .ObterHistoricoAsync(It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        [Trait(nameof(ICriptoMoedaService.ObterHistoricoNegociacoesAsync), "Erro")]
        public async Task ObterHistoricoNegociacoesAsync_Erro()
        {
            await Xunit.Assert.ThrowsAnyAsync<BusinessException>(async () =>
            {
                await criptoMoedaService.ObterHistoricoNegociacoesAsync(string.Empty);
            });
        }

        [TestMethod]
        [Trait(nameof(ICriptoMoedaService.ObterUltimoRegistroNegociacoesAsync), "Sucesso")]
        public async Task ObterUltimoRegistroNegociacoesAsync_Sucesso()
        {
            var siglaCripto = "xpto";

            var retornoUltimoRegistro = new NegociacoesDoDia()
            {
                DataHora = DateTime.Parse("2010-10-10"),
                MaiorPreco = 10,
                MenorPreco = 5,
                MaiorPrecoOfertado = 11,
                MenorPrecoOfertado = 4,
                PrecoUnitario = 10,
                QuantidadeNegociada = 100
            };

            negociacoesDbRepositoryAdapterMock
                .Setup(m => m.ObterAsync(It.IsAny<string>()))
                .Callback<string>(
                (sigla) =>
                {
                    Assert.Equal("xpto", sigla);
                })
                .ReturnsAsync(retornoUltimoRegistro);


            await criptoMoedaService.ObterDadosNegociacoesDoDiaAsync(siglaCripto);

            mercadoBitcoinAdapterMock.Verify(s => s.
            ObterDadosNegociacoesDoDiaAsync(It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        [Trait(nameof(ICriptoMoedaService.ObterUltimoRegistroNegociacoesAsync), "Erro")]
        public async Task ObterUltimoRegistroNegociacoesAsync_Erro()
        {
            await Xunit.Assert.ThrowsAnyAsync<BusinessException>(async () =>
            {
                await criptoMoedaService.ObterUltimoRegistroNegociacoesAsync(string.Empty);
            });
        }
    }
}
