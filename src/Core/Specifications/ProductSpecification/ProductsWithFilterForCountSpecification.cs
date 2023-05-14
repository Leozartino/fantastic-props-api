using Core.Entities;
using FantasticProps.Enums;

namespace Core.Specifications.ProductSpecification;

public class ProductsWithFilterForCountSpecification : BaseSpecification<Product>
{
    public ProductsWithFilterForCountSpecification(SortOptions sort, ProductListRequest productListRequest) 
        : base(product =>
        (!productListRequest.BrandId.HasValue || product.ProductBrandId == productListRequest.BrandId) &&
        (!productListRequest.TypeId.HasValue || product.ProductTypeId == productListRequest.TypeId))
    {
    }
}