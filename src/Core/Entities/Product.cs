namespace Core.Entities
{
    public class Product : BaseEntity
    {
    public string Name { get; private set; }
    public string Description { get; private set; }
    public decimal Price { get; private set; }
    public string PictureUrl { get; private set; }
    public ProductType ProductType { get; private set; }
    public Guid ProductTypeId { get; private set; }
    public ProductBrand ProductBrand { get; private set; }
    public Guid ProductBrandId { get; private set; }
    }
}