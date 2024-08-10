using DbRepositoryAdapter.Entidades;
using Microsoft.EntityFrameworkCore;
namespace DbRepositoryAdapter
{
    public class DbRepositoryAdapterContext : DbContext
    {
        public DbRepositoryAdapterContext(DbContextOptions<DbRepositoryAdapterContext> options) 
            : base(options)
        {
        }

        public DbSet<NegociacoesDbModel> NegociacoesDoDia { get; set; }
        public DbSet<NegociacoesHistoricoDbModel> HistoricoNegociacoes{ get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<NegociacoesHistoricoDbModel>()
                .HasIndex(e => e.Sigla);

            modelBuilder.Entity<NegociacoesDbModel>()
                .HasIndex(e => e.Sigla)
                .IsUnique();

            foreach (var property in modelBuilder.Model.GetEntityTypes()
                            .SelectMany(t => t.GetProperties())
                            .Where(p => p.ClrType == typeof(decimal)))
            {
                property.SetPrecision(14);
                property.SetScale(8);
            }
        }
    }
}
