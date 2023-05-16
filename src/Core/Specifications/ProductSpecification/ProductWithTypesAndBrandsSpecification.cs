using Core.Entities;
using FantasticProps.Enums;

namespace Core.Specifications.ProductSpecification;

public class ProductWithTypesAndBrandsSpecification : BaseSpecification<Product>
{
    public ProductWithTypesAndBrandsSpecification(SortOptions sort, ProductListRequest productListRequest) 
        : base(product =>
            (!productListRequest.BrandId.HasValue || product.ProductBrandId == productListRequest.BrandId) &&
            (!productListRequest.TypeId.HasValue || product.ProductTypeId == productListRequest.TypeId)
        )
    { 
        AddProductTypeAndBrandIncludes();
        ApplyPaging(productListRequest.PageSize * (productListRequest.PageIndex - 1), 
            productListRequest.PageSize);

            switch (sort)
            {
                case SortOptions.PriceAsc:
                    AddOrderBy(product => product.Price);
                    break;
                case SortOptions.PriceDesc:
                    AddOrderByDescending(product => product.Price);
                    break;
                default:
                    AddOrderBy(product => product.Name!);
                    break;
            }
    }

    public ProductWithTypesAndBrandsSpecification(Guid id) : base(product => product.Id == id)
    {
        AddProductTypeAndBrandIncludes();
    }

    private void AddProductTypeAndBrandIncludes()
    {
        AddInclude(product => product.ProductType!);
        AddInclude(product => product.ProductBrand!);
    }
}