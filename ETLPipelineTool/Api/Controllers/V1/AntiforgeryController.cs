using ETLPipelineTool.Application.Dtos.V1.Responses;

namespace ETLPipelineTool.Api.Controllers.V1
{
  [ApiVersion(1.0)]
  public class AntiforgeryController(IAntiforgery antiforgery) : BaseApiController
  {
    [HttpGet()]
    [ProducesResponseType(typeof(AntiforgeryTokenDataDto), StatusCodes.Status200OK)]
    public IActionResult GetAntiforgeryToken()
    {
      var tokens = antiforgery.GetAndStoreTokens(HttpContext);
      return Ok(new AntiforgeryTokenDataDto(tokens.RequestToken!, tokens.HeaderName!));
    }
  }
}
