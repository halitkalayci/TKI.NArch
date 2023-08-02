using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class FileUploadsConfiguration : IEntityTypeConfiguration<FileUploads>
{
    public void Configure(EntityTypeBuilder<FileUploads> builder)
    {
        builder.ToTable("FileUploads").HasKey(fu => fu.Id);

        builder.Property(fu => fu.Id).HasColumnName("Id").IsRequired();
        builder.Property(fu => fu.FileName).HasColumnName("FileName");
        builder.Property(fu => fu.Destination).HasColumnName("Destination");
        builder.Property(fu => fu.Description).HasColumnName("Description");
        builder.Property(fu => fu.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(fu => fu.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(fu => fu.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(fu => !fu.DeletedDate.HasValue);
    }
}