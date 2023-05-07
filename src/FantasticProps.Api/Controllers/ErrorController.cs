using FantasticProps.Errors;
using Microsoft.AspNetCore.Mvc;

namespace FantasticProps.Controllers
{
    [Route("errors/{code}")]
    [ApiController]
    public class ErrorController : BaseApiController
    {
        public IActionResult Error(int code)
        {
            return new ObjectResult(new ApiResponse(code));
        }
    }
}
