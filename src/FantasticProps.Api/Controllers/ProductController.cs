using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications.ProductSpecification;
using Microsoft.AspNetCore.Mvc;

namespace FantasticProps.Controllers
{
  [ApiController]
  [Route("api/products")]
  public class ProductController : ControllerBase
  {
        private readonly IGenericRepository<Product> _productRepository;
        private readonly IGenericRepository<ProductBrand> _productBrandRepository;
        private readonly IGenericRepository<ProductType> _productTypeRepository;

    public ProductController(IGenericRepository<Product> productRepository, 
        IGenericRepository<ProductBrand> productBrandRepository,
        IGenericRepository<ProductType> productTypeRepository)
    {
            _productRepository = productRepository;
            _productBrandRepository = productBrandRepository;
            _productTypeRepository = productTypeRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
    {
            ProductWithTypesAndBrandsSpecification productWithTypesAndBrandsSpecification = new();
            var products = 
                    await _productRepository.ListAsync(productWithTypesAndBrandsSpecification);
            return Ok(products);
    }

    [HttpGet("brands")]
    public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
    {
            return Ok(await _productBrandRepository.ListAllAsync());
    }

    [HttpGet("types")]
    public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
    {
            return Ok(await _productTypeRepository.ListAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProduct(Guid id)
    {
            ProductWithTypesAndBrandsSpecification productWithTypesAndBrandsSpecification = new(id);
            return Ok(await _productRepository.GetEntityWithSpecification(productWithTypesAndBrandsSpecification));
    }
  }

}