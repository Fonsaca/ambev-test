
using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class SaleItemConfiguration : IEntityTypeConfiguration<SaleItem>
{
    public void Configure(EntityTypeBuilder<SaleItem> builder)
    {
        builder.ToTable("SaleItems");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnType("uuid")
            .HasDefaultValueSql("gen_random_uuid()");

        builder.Property(x => x.ProductId)
            //.HasColumnType("uuid")
            .IsRequired();

        builder.Property(x => x.ProductName)
            .HasColumnName("ProductName")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.UnitPrice)
            .HasColumnName("UnitPrice")
            .HasColumnType("decimal(10, 2)")
            .IsRequired();

        builder.Property(x => x.Quantity)
            .IsRequired();

        builder.Property(x => x.IsCanceled)
            .IsRequired();
    }
}