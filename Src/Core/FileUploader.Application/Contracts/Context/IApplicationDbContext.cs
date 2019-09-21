using FileUploader.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace FileUploader.Application.Contracts.Context
{
    public interface IApplicationDbContext
    {
        DbSet<ArticlePrice> ArticlePrices { get; set; }

        DbSet<Article> Articles { get; set; }

        DbSet<Color> Colors { get; set; }

        DbSet<DeliveredIn> DeliveredIns { get; set; }

        DbSet<Section> Sections { get; set; }

        DbSet<Size> Sizes { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}