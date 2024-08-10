using AutoMapper;
using DbRepositoryAdapter.Entidades;
using Domain.Adapters;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DbRepositoryAdapter.Repositories
{
    public class NegociacoesDbRepositoryAdapter : INegociacoesDbRepositoryAdapter
    {
        private readonly DbRepositoryAdapterContext _context;
        private readonly INegociacoesHistoricoDbRepositoryAdapter _historicoDbRepository;
        private readonly IMapper _mapper;


        public NegociacoesDbRepositoryAdapter(DbRepositoryAdapterContext context,
            INegociacoesHistoricoDbRepositoryAdapter negociacoesHistoricoDbRepository,
            IMapper mapper)
        {
            _context = context ??
                throw new ArgumentNullException(nameof(context));

            _historicoDbRepository = negociacoesHistoricoDbRepository ??
                throw new ArgumentNullException(nameof(negociacoesHistoricoDbRepository));

            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        public async Task AtualizarAsync(NegociacoesDoDia negociacoes)
        {
            var model = _mapper.Map<NegociacoesDbModel>(negociacoes);

            var negociacoesExistente = await _context.NegociacoesDoDia
                .Where(x => x.Sigla == model.Sigla)
                .FirstOrDefaultAsync();

            if (negociacoesExistente is null)
                return;

            model.Id = negociacoesExistente.Id;
            _context.Entry(negociacoesExistente).CurrentValues.SetValues(model);
            await _context.SaveChangesAsync();
        }

        public async Task<NegociacoesDoDia> ObterAsync(string sigla)
        {
            var entidade = await _context.NegociacoesDoDia
                .Where(x => x.Sigla == sigla)
                .FirstOrDefaultAsync();

            return _mapper.Map<NegociacoesDoDia>(entidade);
        }

        public async Task SalvarAsync(NegociacoesDoDia negociacoes)
        {
            var model = _mapper.Map<NegociacoesDbModel>(negociacoes);

            model.Id = new Guid();
            await _context.AddAsync(model);
            await _context.SaveChangesAsync();
        }
    }
}
