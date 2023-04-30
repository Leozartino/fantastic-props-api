using System.Linq.Expressions;
using Core.Entities;

namespace Core.Specifications.ProductSpecification;

public class ProductWithTypesAndBrandsSpecification : BaseSpecification<Product>
{
    public ProductWithTypesAndBrandsSpecification()
    {
        AddIncludes();
    }

    public ProductWithTypesAndBrandsSpecification(Guid id) : base(product => product.Id == id)
    {
        AddIncludes();
    }

    private void AddIncludes()
    {
        AddInclude(product => product.ProductType);
        AddInclude(product => product.ProductBrand);
    }
}