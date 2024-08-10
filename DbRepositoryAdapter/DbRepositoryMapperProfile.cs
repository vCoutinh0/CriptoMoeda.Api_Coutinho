using AutoMapper;
using DbRepositoryAdapter.Entidades;
using Domain.Models;

namespace DbRepositoryAdapter
{
    public class DbRepositoryMapperProfile : Profile
    {
        public DbRepositoryMapperProfile() 
        {
            CreateMap<NegociacoesDbModel, NegociacoesDoDia>()
                .ReverseMap();

            CreateMap<NegociacoesHistoricoDbModel, NegociacoesDoDia>()
                .ReverseMap();
        }
    }
}
