using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications.ProductSpecification;
using FantasticProps.Dtos;
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
        private readonly IMapper _mapper;

        public ProductController(IGenericRepository<Product> productRepository, 
        IGenericRepository<ProductBrand> productBrandRepository,
        IGenericRepository<ProductType> productTypeRepository,
        IMapper mapper)
    {
            _productRepository = productRepository;
            _productBrandRepository = productBrandRepository;
            _productTypeRepository = productTypeRepository;
            _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ProductToDto>>> GetProducts()
    {
            ProductWithTypesAndBrandsSpecification productWithTypesAndBrandsSpecification = new();
            var products = 
                    await _productRepository.ListAsync(productWithTypesAndBrandsSpecification);

            return Ok(_mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToDto>>(products));
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
    public async Task<ActionResult<ProductToDto>> GetProduct(Guid id)
    {
            ProductWithTypesAndBrandsSpecification productWithTypesAndBrandsSpecification = new(id);
            
            var product =
                    await _productRepository.GetEntityWithSpecification(productWithTypesAndBrandsSpecification);
            
            return Ok(_mapper.Map<Product, ProductToDto>(product));
    }
  }

}