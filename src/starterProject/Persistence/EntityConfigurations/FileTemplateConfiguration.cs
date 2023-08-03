using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class FileTemplateConfiguration : IEntityTypeConfiguration<FileTemplate>
{
    public void Configure(EntityTypeBuilder<FileTemplate> builder)
    {
        builder.ToTable("FileTemplates").HasKey(ft => ft.Id);

        builder.Property(ft => ft.Id).HasColumnName("Id").IsRequired();
        builder.Property(ft => ft.Content).HasColumnName("Content");
        builder.Property(ft => ft.UserId).HasColumnName("UserId");
        builder.Property(ft => ft.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(ft => ft.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(ft => ft.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(ft => !ft.DeletedDate.HasValue);
    }
}