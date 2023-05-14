using Core;
using Core.Entities;
using FantasticProps.Dtos;
using System.Collections.Generic;
using System.Linq;

namespace FantasticProps.Adapters
{
    public class ProductListAdapter : IAdapter<IEnumerable<Product>, IEnumerable<ProductToDto>>
    {
        public IEnumerable<ProductToDto> Adapt(IEnumerable<Product> source)
        {
            return source.Select(product => new ProductToDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                PictureUrl = product.PictureUrl,
                ProductBrand = product.ProductBrand?.Name,
                ProductType = product.ProductType?.Name
            }).ToList();
        }
    }
}