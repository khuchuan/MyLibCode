using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GrpcClient.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IGreaterClientService _greaterClientService;

        public TestController( IGreaterClientService greaterClientService)
        {
            _greaterClientService = greaterClientService;
        }


        // GET: api/<TestController>
        [HttpGet]
        public string Get( string msg)
        {
            try
            {
                string result = _greaterClientService.CallSayHello(msg);
                return result;
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }


    }
}
