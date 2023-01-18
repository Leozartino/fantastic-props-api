using Microsoft.AspNetCore.Mvc;

namespace FantasticProps.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class ProductsController : ControllerBase
  {
    [HttpGet]
    public string GetProducts()
    {
      return "this will be a list of products";
    }

    [HttpGet("{id}")]
    public string GetProduct()
    {
      return "single product";
    }
  }

}