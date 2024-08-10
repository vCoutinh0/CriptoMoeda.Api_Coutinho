using Domain.Models;

namespace Domain.Adapters
{
    public interface INegociacoesDbRepositoryAdapter
    {
        /// <summary>
        ///     Dados do ultimo registro de negociações de uma criptomoeda especifica.
        /// </summary>
        /// <param name="siglaMoeda">Sigla da criptomoeda que deseja obter dados.</param>
        /// <returns>
        ///     Informações com o resumo do ultimo registro de negociações.
        /// </returns>
        Task<NegociacoesDoDia> ObterAsync(string sigla);

        /// <summary>
        ///     Salva os dados de negociações de uma criptomoeda especifica.
        /// </summary>
        /// <param name="negociacoes">Dados de negociacoes</param>
        Task SalvarAsync(NegociacoesDoDia criptomoeda);

        /// <summary>
        ///     Atualiza os dados de negociações de uma criptomoeda especifica.
        /// </summary>
        /// <param name="negociacoes">Dados de negociacoes</param>
        Task AtualizarAsync(NegociacoesDoDia criptomoeda);
    }
}
