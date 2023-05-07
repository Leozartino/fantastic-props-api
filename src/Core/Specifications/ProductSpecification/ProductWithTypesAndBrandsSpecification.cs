using Core.Entities;

namespace Core.Specifications.ProductSpecification;

public class ProductWithTypesAndBrandsSpecification : BaseSpecification<Product>
{
    public ProductWithTypesAndBrandsSpecification() : base()
    {
        AddProductTypeAndBrandIncludes();
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