using Microsoft.AspNetCore.Mvc;

namespace TSLServer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BroadcastController : ControllerBase
    {
        [HttpGet("handshake")]
        public IActionResult InitialHandShake()
        {
            return Ok();
        }
    }
}
