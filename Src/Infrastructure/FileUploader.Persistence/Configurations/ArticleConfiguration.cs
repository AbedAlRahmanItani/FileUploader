using FileUploader.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FileUploader.Persistence.Configurations
{
    public class ArticleConfiguration : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.HasKey(e => e.Code);

            builder.Property(e => e.Code).HasMaxLength(25);

            builder.Property(e => e.Label).IsRequired().HasMaxLength(50);

            builder.Property(e => e.Description).IsRequired().HasMaxLength(50);
        }
    }
}
