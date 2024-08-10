using Domain.Models;

namespace Domain.Services
{
    public interface ICriptoMoedaService
    {
        /// <summary>
        ///     Dados das últimas 24 horas de negociações de uma criptomoeda especifica.
        /// </summary>
        /// <param name="siglaMoeda">Sigla da criptomoeda que deseja obter dados.</param>
        /// <returns>
        ///     Informações com o resumo das últimas 24 horas de negociações.
        /// </returns>
        Task<NegociacoesDoDia> ObterDadosNegociacoesDoDiaAsync(string siglaMoeda);

        /// <summary>
        ///     Histórico das negociações de uma criptomoeda especifica. A cada busca por uma criptmoeda o historico é atualizado com os dados das últimas 24 horas.
        /// </summary>
        /// <param name="siglaMoeda">Sigla da criptomoeda que deseja obter dados.</param>
        /// <returns>
        ///     Informações com o histórico das negociacoes
        /// </returns>
        Task<IEnumerable<NegociacoesDoDia>> ObterHistoricoNegociacoesAsync(string siglaMoeda);

        /// <summary>
        ///     Dados do ultimo registro de negociações de uma criptomoeda especifica.
        /// </summary>
        /// <param name="siglaMoeda">Sigla da criptomoeda que deseja obter dados.</param>
        /// <returns>
        ///     Informações com o resumo do ultimo registro de negociações.
        /// </returns>
        Task<NegociacoesDoDia> ObterUltimoRegistroNegociacoesAsync(string siglaMoeda);
    }
}
