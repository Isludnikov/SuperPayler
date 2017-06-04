using System.Web.Http;

namespace SuperPayler.Controllers
{
    public class AliveController : ApiController
    {
        // GET api/values
        [HttpGet]
        public string IsAlive() => "Server alive";
    }
}
