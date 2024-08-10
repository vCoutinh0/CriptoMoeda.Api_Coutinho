using Domain.Models;

namespace Domain.Adapters
{
    public interface INegociacoesHistoricoDbRepositoryAdapter
    {
        /// <summary>
        ///     Registra os dados de negociacoes como histórico de uma criptmoeda específica.
        /// </summary>
        /// <param name="negociacoes">Dados de negociacoes</param>
        Task RegistrarNegociacoesHistoricoAsync(NegociacoesDoDia negociacoes);

        /// <summary>
        ///     Histórico das negociações de uma criptomoeda especifica.
        /// </summary>
        /// <param name="siglaMoeda">Sigla da criptomoeda que deseja obter dados.</param>
        /// <returns>
        ///     Informações com o histórico das negociacoes
        /// </returns>
        Task<IEnumerable<NegociacoesDoDia>> ObterHistoricoAsync(string sigla);
    }
}
