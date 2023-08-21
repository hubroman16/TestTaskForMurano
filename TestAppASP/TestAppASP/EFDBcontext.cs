using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using TestAppASP.Models;

namespace TestAppASP
{
    public class EFDBContext : DbContext
    {
        public DbSet<SearchRequest> SearchRequest { get; set; }

        public EFDBContext(DbContextOptions<EFDBContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SearchRequest>(entity =>
            {
                entity.HasKey(e => new { e.Request, e.URL });
            });
        }
    }

    /// <summary>
    /// For Migrations
    /// </summary>
    public class EFDBContextFactory : IDesignTimeDbContextFactory<EFDBContext>
    {
        public EFDBContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<EFDBContext>();
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=SearchDB;Trusted_Connection=True;MultipleActiveResultSets=true", b => b.MigrationsAssembly("TestAppASP"));

            return new EFDBContext(optionsBuilder.Options);
        }
    }
}