namespace Core.Entities
{
    public class Product : BaseEntity
    {
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string PictureUrl { get; set; }
    public ProductType ProductType { get; set; }
    public Guid ProductTypeId { get; set; }
    public ProductBrand ProductBrand { get; set; }
    public Guid ProductBrandId { get; set; }
    }
}