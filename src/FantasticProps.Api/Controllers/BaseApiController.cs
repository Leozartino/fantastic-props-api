using Core.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace FantasticProps.Controllers;

[ApiController]
[Route("api/[controller]")]
public abstract class BaseApiController : ControllerBase
{
    protected ActionResult CustomResponse(object result = null)
    {
        if (ValidtOperation())
        {
            return Ok(new ResultResponse<object>
            {
                Success = true,
                Data = result
            });
        }

        return BadRequest(new ResultResponse<object>
        {
            Success = false,
            Errors = GetErrors()
        }); 
    }

    public bool ValidtOperation()
    {
        return true;
    }

    protected List<string> GetErrors()
    {
        return new List<string> { "error1", "error2" };
    }
}


