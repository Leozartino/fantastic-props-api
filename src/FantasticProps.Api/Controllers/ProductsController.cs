using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Core.Specifications.ProductSpecification;
using FantasticProps.Dtos;
using FantasticProps.Enums;
using FantasticProps.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FantasticProps.Controllers
{
    public class ProductsController : BaseApiController
    {
        private readonly IGenericRepository<Product> _productRepository;
        private readonly IGenericRepository<ProductBrand> _productBrandRepository;
        private readonly IGenericRepository<ProductType> _productTypeRepository;
        private readonly IMapper _mapper;

        public ProductsController(IGenericRepository<Product> productRepository,
        IGenericRepository<ProductBrand> productBrandRepository,
        IGenericRepository<ProductType> productTypeRepository,
        IMapper mapper)
        {
            _productRepository = productRepository;
            _productBrandRepository = productBrandRepository;
            _productTypeRepository = productTypeRepository;
            _mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IReadOnlyList<ProductToDto>>> GetProducts([FromBody] ProductListRequest request)
        {
            if (!Enum.TryParse(request.Sort, true, out SortOptions sortOptions))
                return BadRequest(new ApiResponse(400, "Invalid sort options"));

            ProductWithTypesAndBrandsSpecification productWithTypesAndBrandsSpecification = new(sortOptions, request);
            var products =
                    await _productRepository.ListAsync(productWithTypesAndBrandsSpecification);

            return Ok(_mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToDto>>(products));
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

            return Ok(_mapper.Map<Product, ProductToDto>(product));
        }
    }

}