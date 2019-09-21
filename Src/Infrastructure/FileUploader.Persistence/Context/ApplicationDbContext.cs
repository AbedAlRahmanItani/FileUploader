using FileUploader.Application.Contracts.Context;
using FileUploader.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FileUploader.Persistence.Context
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }

        public DbSet<ArticlePrice> ArticlePrices { get; set; }

        public DbSet<Article> Articles { get; set; }

        public DbSet<Color> Colors { get; set; }

        public DbSet<DeliveredIn> DeliveredIns { get; set; }

        public DbSet<Section> Sections { get; set; }

        public DbSet<Size> Sizes { get; set; }
    }
}