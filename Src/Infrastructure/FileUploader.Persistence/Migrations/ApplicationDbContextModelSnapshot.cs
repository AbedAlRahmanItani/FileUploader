﻿// <auto-generated />
using FileUploader.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FileUploader.Persistence.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FileUploader.Domain.Entities.Article", b =>
                {
                    b.Property<string>("Code")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(25);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Label")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Code");

                    b.ToTable("Articles");
                });

            modelBuilder.Entity("FileUploader.Domain.Entities.ArticlePrice", b =>
                {
                    b.Property<string>("Key")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(25);

                    b.Property<string>("ArticleCode")
                        .IsRequired()
                        .HasMaxLength(25);

                    b.Property<string>("ColorCode")
                        .IsRequired()
                        .HasMaxLength(25);

                    b.Property<string>("DeliveredInCode")
                        .IsRequired()
                        .HasMaxLength(25);

                    b.Property<decimal>("DiscountPrice");

                    b.Property<decimal>("Price");

                    b.Property<string>("SectionCode")
                        .IsRequired()
                        .HasMaxLength(25);

                    b.Property<string>("SizeCode")
                        .IsRequired()
                        .HasMaxLength(25);

                    b.HasKey("Key");

                    b.HasIndex("ArticleCode");

                    b.HasIndex("ColorCode");

                    b.HasIndex("DeliveredInCode");

                    b.HasIndex("SectionCode");

                    b.HasIndex("SizeCode");

                    b.ToTable("ArticlePrices");
                });

            modelBuilder.Entity("FileUploader.Domain.Entities.Color", b =>
                {
                    b.Property<string>("Code")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(25);

                    b.HasKey("Code");

                    b.ToTable("Colors");
                });

            modelBuilder.Entity("FileUploader.Domain.Entities.DeliveredIn", b =>
                {
                    b.Property<string>("Code")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(25);

                    b.HasKey("Code");

                    b.ToTable("DeliveredIns");
                });

            modelBuilder.Entity("FileUploader.Domain.Entities.Section", b =>
                {
                    b.Property<string>("Code")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(25);

                    b.HasKey("Code");

                    b.ToTable("Sections");
                });

            modelBuilder.Entity("FileUploader.Domain.Entities.Size", b =>
                {
                    b.Property<string>("Code")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(25);

                    b.HasKey("Code");

                    b.ToTable("Sizes");
                });

            modelBuilder.Entity("FileUploader.Domain.Entities.ArticlePrice", b =>
                {
                    b.HasOne("FileUploader.Domain.Entities.Article", "Article")
                        .WithMany("ArticlePrices")
                        .HasForeignKey("ArticleCode")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FileUploader.Domain.Entities.Color", "Color")
                        .WithMany("ArticlePrices")
                        .HasForeignKey("ColorCode")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FileUploader.Domain.Entities.DeliveredIn", "DeliveredIn")
                        .WithMany("ArticlePrices")
                        .HasForeignKey("DeliveredInCode")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FileUploader.Domain.Entities.Section", "Section")
                        .WithMany("ArticlePrices")
                        .HasForeignKey("SectionCode")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FileUploader.Domain.Entities.Size", "Size")
                        .WithMany("ArticlePrices")
                        .HasForeignKey("SizeCode")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
