using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SurveyApp.Application.Features.Brands;

namespace SurveyApp.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BrandsController : BaseController
{
	[Authorize(Roles = "Admin")]
	[HttpPost("add")]
	public async Task<IActionResult> Add([FromBody] CreateBrandCommand createBrandCommand)
	{
		var authHeader = HttpContext.Request.Headers["Authorization"].ToString();

		// Örnek: JWT’den claimlere erişmek
		var userClaims = HttpContext.User.Claims.Select(c => new { c.Type, c.Value }).ToList();
		CreatedBrandResponse response = await Mediator.Send(createBrandCommand);
		return Ok(response);
	}
}