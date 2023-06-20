using Application.Features.Brands.Queries.GetList;
using Core.Application.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class BrandsController : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery]PageRequest pageRequest)
    {
        GetListBrandQuery query = new()
        {
            PageRequest = pageRequest
        };
        var response = await Mediator.Send(query);
        return Ok(response);
    }
}
