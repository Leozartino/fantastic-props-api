using Core.Entities;

namespace Core.Specifications.ProductSpecification;

public class ProductWithTypesAndBrandsSpecification : BaseSpecification<Product>
{
    public ProductWithTypesAndBrandsSpecification(string sort) : base()
    {
        AddProductTypeAndBrandIncludes();
        AddOrderBy(product => product.Name);

        if (!string.IsNullOrEmpty(sort))
        {
            switch (sort)
            {
                case"priceAsc":
                    AddOrderBy(product => product.Price);
                    break;
                case"priceDesc":
                    AddOrderByDescending(product => product.Price);
                    break;
                default:
                    AddOrderBy(product => product.Name);
                    break;
            }
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