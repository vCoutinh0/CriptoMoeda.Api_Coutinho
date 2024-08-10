using AutoMapper;
using DbRepositoryAdapter.Entidades;
using Domain.Adapters;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DbRepositoryAdapter.Repositories
{
    public class NegociacoesHistoricoDbRepositoryAdapter : INegociacoesHistoricoDbRepositoryAdapter
    {
        private readonly DbRepositoryAdapterContext _context;
        private readonly IMapper _mapper;

        public NegociacoesHistoricoDbRepositoryAdapter(DbRepositoryAdapterContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<NegociacoesDoDia>> ObterHistoricoAsync(string sigla)
        {
            var registroNegociacoes = await _context.HistoricoNegociacoes
                                        .Where(x => x.Sigla == sigla)
                                        .OrderByDescending(x => x.DataHora)
                                        .ToListAsync();

            return _mapper.Map<List<NegociacoesDoDia>>(registroNegociacoes);
        }

        public async Task RegistrarNegociacoesHistoricoAsync(NegociacoesDoDia negociacoes)
        {
            var model = _mapper.Map<NegociacoesHistoricoDbModel>(negociacoes);
            _context.HistoricoNegociacoes.Add(model);
            await _context.SaveChangesAsync();
        }
    }
}
