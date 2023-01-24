using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FantasticProps.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class ProductController : ControllerBase
  {
    private readonly IProductRepository _productRepository;
    public ProductController(IProductRepository productRepository)
    {
      _productRepository = productRepository;
    }

    [HttpGet]
    public async Task<ActionResult<List<Product>>> GetProducts()
    {
      return Ok(await _productRepository.GetProductsAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
      return Ok(await _productRepository.GetProductByIdAsync(id));
    }
  }

}