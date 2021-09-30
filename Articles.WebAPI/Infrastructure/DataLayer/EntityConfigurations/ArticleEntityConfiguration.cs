using Articles.WebAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.CodeAnalysis;

namespace Articles.WebAPI.Infrastructure.DataLayer.EntityConfigurations
{
    [ExcludeFromCodeCoverage]
    public class ArticleEntityConfiguration : IEntityTypeConfiguration<ArticleEntity>
    {
        public void Configure(EntityTypeBuilder<ArticleEntity> builder)
        {
            builder.HasKey(x => x.ArticleId);
            builder.Property(x => x.ArticleId).ValueGeneratedOnAdd();

            builder.ToTable("Articles", "dbo");
        }
    }
}