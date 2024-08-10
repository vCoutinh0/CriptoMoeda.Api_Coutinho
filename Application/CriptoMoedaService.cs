using Domain.Adapters;
using Domain.Exceptions;
using Domain.Models;
using Domain.Services;

namespace Application
{
    public class CriptoMoedaService : ICriptoMoedaService
    {
        private readonly IMercadoBitcoinAdapter _mercadoBitcoinAdapter;
        private readonly INegociacoesDbRepositoryAdapter _negociacoesDbRepositoryAdapter;
        private readonly INegociacoesHistoricoDbRepositoryAdapter _negociacoesHistoricoDbRepositoryAdapter;

        public CriptoMoedaService(IMercadoBitcoinAdapter mercadoBitcoinAdapter, 
            INegociacoesDbRepositoryAdapter negociacoesDbRepositoryAdapter,
            INegociacoesHistoricoDbRepositoryAdapter negociacoesHistoricoDbRepositoryAdapter)
        {
            _mercadoBitcoinAdapter = mercadoBitcoinAdapter ??
                throw new ArgumentNullException(nameof(mercadoBitcoinAdapter));

            _negociacoesDbRepositoryAdapter = negociacoesDbRepositoryAdapter ??
                throw new ArgumentNullException(nameof(negociacoesDbRepositoryAdapter));

            _negociacoesHistoricoDbRepositoryAdapter = negociacoesHistoricoDbRepositoryAdapter ??
                throw new ArgumentNullException(nameof(negociacoesHistoricoDbRepositoryAdapter));
        }

        public async Task<NegociacoesDoDia> ObterDadosNegociacoesDoDiaAsync(string siglaMoeda)
        {
            if (string.IsNullOrWhiteSpace(siglaMoeda))
                throw new Exception("Sigla escolhida não é valida.");

            return await _mercadoBitcoinAdapter.ObterDadosNegociacoesDoDiaAsync(siglaMoeda);
        }

        public async Task<IEnumerable<NegociacoesDoDia>> ObterHistoricoNegociacoesAsync(string siglaMoeda)
        {
            if (string.IsNullOrWhiteSpace(siglaMoeda))
                throw new BusinessException("Sigla escolhida não é valida.");

            await AtualizarHistoricoNegociacoesAsync(siglaMoeda);

            return await _negociacoesHistoricoDbRepositoryAdapter.ObterHistoricoAsync(siglaMoeda);
        }

        public async Task<NegociacoesDoDia> ObterUltimoRegistroNegociacoesAsync(string siglaMoeda)
        {
            if (string.IsNullOrWhiteSpace(siglaMoeda))
                throw new BusinessException("Sigla escolhida não é valida.");

            return await _negociacoesDbRepositoryAdapter.ObterAsync(siglaMoeda);
        }

        private async Task AtualizarHistoricoNegociacoesAsync(string siglaMoeda)
        {
            var negociacaoDoDia = await _mercadoBitcoinAdapter.ObterDadosNegociacoesDoDiaAsync(siglaMoeda);
            negociacaoDoDia.Sigla = siglaMoeda;

            var existeRegistro = await _negociacoesDbRepositoryAdapter.ObterAsync(siglaMoeda);

            if (existeRegistro is null)
            {
                await _negociacoesDbRepositoryAdapter.SalvarAsync(negociacaoDoDia);
            }
            else
            {
                await _negociacoesDbRepositoryAdapter.AtualizarAsync(negociacaoDoDia);
            }

            await _negociacoesHistoricoDbRepositoryAdapter.RegistrarNegociacoesHistoricoAsync(negociacaoDoDia);
        }
    }
}