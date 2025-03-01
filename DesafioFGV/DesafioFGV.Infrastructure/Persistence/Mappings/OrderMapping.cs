using DesafioFGV.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DesafioFGV.Infrastructure.Persistence.Mappings;

public class OrderMapping : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(o => o.Id);

        builder.Property(o => o.Description)
            .HasMaxLength(500)
            .IsRequired()
            .UseCollation("SQL_Latin1_General_CP1_CI_AS");

        builder.Property(o => o.Value)
            .IsRequired();

        builder.Property(o => o.DateOrder)
            .IsRequired();

        builder.Property(o => o.CreatedAt)
            .IsRequired();

        builder.Property(o => o.UpdatedAt);

        builder.Property(o => o.DeletedAt);

        builder.Property(o => o.IsDeleted)
            .IsRequired();

        builder.HasOne(o => o.User)
            .WithMany()
            .HasForeignKey(o => o.IdUser)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.ToTable("Order");
    }
}
