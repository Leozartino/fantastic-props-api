using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
  public class ProductConfiguration : IEntityTypeConfiguration<Product>
  {
    public void Configure(EntityTypeBuilder<Product> builder)
    {
      builder.Property(property => property.Name).IsRequired().HasMaxLength(110);
      builder.Property(property => property.Description).IsRequired();
      builder.Property(property => property.Price).HasColumnType("decimal(18,2)");
      builder.Property(property => property.PictureUrl).IsRequired();
      builder.HasOne(brand => brand.ProductBrand).WithMany()
        .HasForeignKey(property => property.ProductBrandId);
      builder.HasOne(type => type.ProductType).WithMany()
        .HasForeignKey(property => property.ProductTypeId);
    }
  }
}