using System.Web.Http;
using System.Reflection;

namespace EnovaApiService.Controllers
{
    public class SystemController : ApiController
    {
        [HttpGet]
        [Route("api/system/version")]
        public string Get()
        {
            return $"Enova Api ver: {Assembly.GetExecutingAssembly().GetName().Version}";
        }
    }
}
