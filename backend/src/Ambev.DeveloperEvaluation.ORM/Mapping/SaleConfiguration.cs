
using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class SaleConfiguration : IEntityTypeConfiguration<Sale>
{
    public void Configure(EntityTypeBuilder<Sale> builder)
    {
        builder.ToTable("Sales");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("Id")
            .HasColumnType("uuid")
            .HasDefaultValueSql("gen_random_uuid()");

        builder.Property(x => x.Branch)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.CustomerId)
            .HasColumnType("uuid")
            .IsRequired();

        builder.Property(x => x.CustomerName)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.SoldOn)
            .IsRequired();

        builder.Property(x => x.UpdatedAt);

        builder.HasMany(x => x.Items)
            .WithOne()
            .HasForeignKey("SaleId");
    }
}