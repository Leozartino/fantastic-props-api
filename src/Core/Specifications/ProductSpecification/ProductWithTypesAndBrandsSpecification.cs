using Core.Entities;
using FantasticProps.Enums;

namespace Core.Specifications.ProductSpecification;

public class ProductWithTypesAndBrandsSpecification : BaseSpecification<Product>
{
    public ProductWithTypesAndBrandsSpecification(SortOptions sort,
        Guid? brandId, Guid? typeId) : base(product =>
            (!brandId.HasValue || product.ProductBrandId == brandId) &&
            (!typeId.HasValue || product.ProductTypeId == typeId)
        )
    { 
        AddProductTypeAndBrandIncludes();

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