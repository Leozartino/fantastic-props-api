using Core.Entities;
using FantasticProps.Enums;
using System.Diagnostics;

namespace Core.Specifications.ProductSpecification;

public class ProductWithTypesAndBrandsSpecification : BaseSpecification<Product>
{
    public ProductWithTypesAndBrandsSpecification(SortOptions sort) : base()
    {
        AddProductTypeAndBrandIncludes();
        AddOrderBy(product => product.Name!);
        Debug.WriteLine($"SortOptions: {sort}");
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