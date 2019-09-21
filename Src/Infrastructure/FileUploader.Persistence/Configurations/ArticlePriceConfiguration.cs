using FileUploader.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FileUploader.Persistence.Configurations
{
    public class ArticlePriceConfiguration : IEntityTypeConfiguration<ArticlePrice>
    {
        public void Configure(EntityTypeBuilder<ArticlePrice> builder)
        {
            builder.HasKey(e => e.Key);

            builder.Property(e => e.Key).HasMaxLength(25);

            builder.Property(e => e.Price).IsRequired();

            builder.Property(e => e.ArticleCode).IsRequired().HasMaxLength(25);

            builder.Property(e => e.DeliveredInCode).IsRequired().HasMaxLength(25);

            builder.Property(e => e.SectionCode).IsRequired().HasMaxLength(25);

            builder.Property(e => e.SizeCode).IsRequired().HasMaxLength(25);

            builder.Property(e => e.ColorCode).IsRequired().HasMaxLength(25);
            
            builder.HasOne(d => d.Article)
                .WithMany(p => p.ArticlePrices)
                .HasForeignKey(d => d.ArticleCode);

            builder.HasOne(d => d.DeliveredIn)
                .WithMany(p => p.ArticlePrices)
                .HasForeignKey(d => d.DeliveredInCode);

            builder.HasOne(d => d.Section)
                .WithMany(p => p.ArticlePrices)
                .HasForeignKey(d => d.SectionCode);

            builder.HasOne(d => d.Size)
                .WithMany(p => p.ArticlePrices)
                .HasForeignKey(d => d.SizeCode);

            builder.HasOne(d => d.Color)
                .WithMany(p => p.ArticlePrices)
                .HasForeignKey(d => d.ColorCode);
        }
    }
}
