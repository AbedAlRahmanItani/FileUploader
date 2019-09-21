using FileUploader.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FileUploader.Persistence.Configurations
{
    public class DeliveredInConfiguration : IEntityTypeConfiguration<DeliveredIn>
    {
        public void Configure(EntityTypeBuilder<DeliveredIn> builder)
        {
            builder.HasKey(e => e.Code);

            builder.Property(e => e.Code).HasMaxLength(25);
        }
    }
}
