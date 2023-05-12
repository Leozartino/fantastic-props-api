using Core;
using Core.Entities;
using FantasticProps.Dtos;
using System.Collections.Generic;

namespace FantasticProps.Adapters
{
    public class ProductListAdapter : IAdapter<IEnumerable<Product>, IEnumerable<ProductToDto>>
    {
        public IEnumerable<ProductToDto> Adapt(IEnumerable<Product> source)
        {
            List<ProductToDto> productToDtos = new();

            foreach (Product product in source)
            {
                ProductToDto productToDto = new()
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    PictureUrl = product.PictureUrl,
                    ProductBrand = product.ProductBrand?.Name,
                    ProductType = product.ProductType?.Name
                };

                productToDtos.Add(productToDto);
            }

            return productToDtos;
        }
    }
}
