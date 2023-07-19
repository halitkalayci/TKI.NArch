using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class GroupTreeContentConfiguration : IEntityTypeConfiguration<GroupTreeContent>
{
    public void Configure(EntityTypeBuilder<GroupTreeContent> builder)
    {
        builder.ToTable("GroupTreeContents").HasKey(gtc => gtc.Id);

        builder.Property(gtc => gtc.Id).HasColumnName("Id").IsRequired();
        builder.Property(gtc => gtc.Title).HasColumnName("Title");
        builder.Property(gtc => gtc.Target).HasColumnName("Target");
        builder.Property(gtc => gtc.Icon).HasColumnName("Icon");
        builder.Property(gtc => gtc.RowOrder).HasColumnName("RowOrder");
        builder.Property(gtc => gtc.ShowOnAuth).HasColumnName("ShowOnAuth");
        builder.Property(gtc => gtc.HideOnAuth).HasColumnName("HideOnAuth");
        builder.Property(gtc => gtc.ParentId).HasColumnName("ParentId");
        builder.Property(gtc => gtc.Type).HasColumnName("Type");
        builder.Property(gtc => gtc.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(gtc => gtc.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(gtc => gtc.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(gtc => !gtc.DeletedDate.HasValue);
    }
}