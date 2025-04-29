using Microsoft.AspNetCore.Mvc;

namespace ETLPipelineTool.Api.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public abstract class BaseApiController : ControllerBase { }
}
