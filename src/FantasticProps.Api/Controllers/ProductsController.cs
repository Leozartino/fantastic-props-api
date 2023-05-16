using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Core.Specifications.ProductSpecification;
using FantasticProps.Dtos;
using FantasticProps.Enums;
using FantasticProps.Errors;
using FantasticProps.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FantasticProps.Controllers
{
    public class ProductsController : BaseApiController
    {
        private readonly IGenericRepository<Product> _productRepository;
        private readonly IGenericRepository<ProductBrand> _productBrandRepository;
        private readonly IGenericRepository<ProductType> _productTypeRepository;
        private readonly IAdapter<IEnumerable<Product>, IEnumerable<ProductToDto>> _productListAdapter;
        private readonly IAdapter<Product, ProductToDto> _productDtoAdapter;

        public ProductsController(IGenericRepository<Product> productRepository,
        IGenericRepository<ProductBrand> productBrandRepository,
        IGenericRepository<ProductType> productTypeRepository,
        IAdapter<IEnumerable<Product>, IEnumerable<ProductToDto>> productListAdapter,
        IAdapter<Product, ProductToDto> productDtoAdapter
        )
        {
            _productRepository = productRepository;
            _productBrandRepository = productBrandRepository;
            _productTypeRepository = productTypeRepository;
            _productListAdapter = productListAdapter;
            _productDtoAdapter = productDtoAdapter;
            
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Pagination<ProductToDto>>> GetProducts([FromBody] ProductListRequest request)
        {
            if (!Enum.TryParse(request.Sort, true, out SortOptions sortOptions))
                return BadRequest(new ApiResponse(400, "Invalid sort options"));

            var productWithTypesAndBrandsSpecification = new ProductWithTypesAndBrandsSpecification(sortOptions, request);
            var countSpecification = new ProductsWithFilterForCountSpecification(sortOptions, request);
            var totalItems = await _productRepository.CountAsync(countSpecification);
            
            var products =
                    await _productRepository.ListAsync(productWithTypesAndBrandsSpecification);

            var paginationData = new Pagination<ProductToDto>
            {
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Count = totalItems,
                Data = _productListAdapter.Adapt(products)
            };

            return Ok(paginationData);
        }

        [HttpGet("brands")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {
            return Ok(await _productBrandRepository.ListAllAsync());
        }

        [HttpGet("types")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
        {
            return Ok(await _productTypeRepository.ListAllAsync());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ProductToDto>> GetProduct([FromRoute] Guid id)
        {
            ProductWithTypesAndBrandsSpecification productWithTypesAndBrandsSpecification = new(id);

            var product =
                    await _productRepository.GetEntityWithSpecification(productWithTypesAndBrandsSpecification);

            if(product is null) return NotFound(new ApiResponse(404));

            return Ok(_productDtoAdapter.Adapt(product));
        }
    }

}