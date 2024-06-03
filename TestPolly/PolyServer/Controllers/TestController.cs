using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PollyServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        public string Get(int x)
        {
            return "Hello from PollyServer!";
        }
    }
}
