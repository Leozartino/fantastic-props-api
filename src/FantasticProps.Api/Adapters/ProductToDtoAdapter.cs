using Core;
using Core.Entities;
using FantasticProps.Dtos;

namespace FantasticProps.Adapters
{
    public class ProductToDtoAdapter : IAdapter<Product, ProductToDto>
    {
        public ProductToDto Adapt(Product source)
        {
            return new ProductToDto
            {
                Id = source.Id,
                Name = source.Name,
                Description = source.Description,
                Price = source.Price,
                PictureUrl = source.PictureUrl,
                ProductBrand = source.ProductBrand?.Name,
                ProductType = source.ProductType?.Name
            };
        }
    }
}
